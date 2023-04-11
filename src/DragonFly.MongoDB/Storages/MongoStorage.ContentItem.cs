// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;
using DragonFly.Query;
using DragonFly.Storage;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IContentStorage
{
    public async Task<ContentItem?> GetContentAsync(string schema, Guid id)
    {
        ContentSchema contentSchema = await GetSchemaAsync(schema);
        IMongoCollection<MongoContentItem> collection = GetMongoCollection(schema);

        MongoContentItem? result = collection.AsQueryable().FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            return null;
            //throw new Exception($"ContentItem '{schema}/{id}' not found");
        }

        return result.ToModel(contentSchema);
    }

    public IMongoCollection<MongoContentItem> GetMongoCollection(ContentSchema schema, bool published = false)
    {
        return GetMongoCollection(schema.Name, published);
    }

    public IMongoCollection<MongoContentItem> GetMongoCollection(string type, bool published = false)
    {
        string name = $"ContentItems_{type}";

        if (published)
        {
            name += "_Published";
        }

        if (ContentItems.TryGetValue(name, out IMongoCollection<MongoContentItem>? items) == false)
        {
            items = Database.GetCollection<MongoContentItem>(name);

            ContentItems.Add(name, items);
        }

        return items;
    }

    public async Task<QueryResult<ContentItem>> QueryAsync(ContentQuery query)
    {
        ContentSchema schema = await GetSchemaAsync(query.Schema);
        IMongoCollection<MongoContentItem> collection = GetMongoCollection(schema.Name, query.Published);

        List<FilterDefinition<MongoContentItem>> filters = new List<FilterDefinition<MongoContentItem>>();

        //filter all fields
        if (string.IsNullOrEmpty(query.SearchPattern) == false)
        {
            List<FilterDefinition<MongoContentItem>> patternQuery = new List<FilterDefinition<MongoContentItem>>();

            foreach (string field in schema.Fields
                                    .Where(x => x.Value.FieldType == ContentFieldManager.Default.GetContentFieldName<StringField>())
                                    .Select(x => x.Key))
            {
                patternQuery.Add(Builders<MongoContentItem>.Filter.Regex($"{nameof(MongoContentItem.Fields)}.{field}", new BsonRegularExpression(query.SearchPattern, "i")));
            }

            if (patternQuery.Count > 1)
            {
                filters.Add(Builders<MongoContentItem>.Filter.Or(patternQuery));
            }
            else if (patternQuery.Count == 1)
            {
                filters.Add(patternQuery.First());
            }
            else
            {

            }
        }

        FindOptions<MongoContentItem> findOptions = new FindOptions<MongoContentItem>()
        {
            Limit = query.Top,
            Skip = query.Skip
        };

        //use default order?
        if (query.OrderFields.Any() == false)
        {
            query.OrderFields = schema.OrderFields.ToList();
        }

        //sort
        if (query.OrderFields.Any())
        {
            IList<SortDefinition<MongoContentItem>> sorts = new List<SortDefinition<MongoContentItem>>();

            //order
            foreach (FieldOrder orderField in query.OrderFields)
            {
                if (orderField.Asc)
                {
                    sorts.Add(Builders<MongoContentItem>.Sort.Ascending(orderField.Name));
                }
                else
                {
                    sorts.Add(Builders<MongoContentItem>.Sort.Descending(orderField.Name));
                }
            }

            findOptions.Sort = Builders<MongoContentItem>.Sort.Combine(sorts);
        }

        //projection
        if (query.IncludeListFieldsOnly && schema.ListFields.Any())
        {
            ProjectionDefinition<MongoContentItem> p = Builders<MongoContentItem>.Projection.Include(x => x.Id);

            foreach (string field in schema.ListFields)
            {
                p = p.Include($"{nameof(MongoContentItem.Fields)}.{field}");
            }

            findOptions.Projection = p;
        }

        //fields
        FieldQueryActionContext converterContext = new FieldQueryActionContext();

        foreach (FieldQuery f in query.Fields)
        {
            IFieldQueryAction queryAction = MongoQueryManager.Default.GetByType(f.GetType());

            queryAction.Apply(f, converterContext);
        }

        filters.AddRange(converterContext.Filters);

        //used assets
        if (query.UsedAsset != null)
        {
            string assetFieldName = ContentFieldManager.Default.GetContentFieldName<AssetField>();

            List<FilterDefinition<MongoContentItem>> assetQueries = new List<FilterDefinition<MongoContentItem>>();

            foreach (var assetField in schema.Fields.Where(x => x.Value.FieldType == assetFieldName))
            {
                assetQueries.Add(Builders<MongoContentItem>.Filter.Eq($"{nameof(MongoContentItem.Fields)}.{assetField.Key}", query.UsedAsset.Value));
            }

            filters.Add(Builders<MongoContentItem>.Filter.Or(assetQueries));
        }

        //bundle filter definitions
        FilterDefinition<MongoContentItem> q = Builders<MongoContentItem>.Filter.Empty;

        if (filters.Count >= 2)
        {
            q = Builders<MongoContentItem>.Filter.And(filters);
        }
        else if (filters.Count == 1)
        {
            q = filters.First();
        }

        findOptions.Collation = new Collation(locale: "en", strength: CollationStrength.Primary);

        //execute query
        var cursor = await collection.FindAsync(q, findOptions);

        long totalCount = await collection.CountDocumentsAsync(q);

        IList<MongoContentItem> result = await cursor.ToListAsync();

        QueryResult<ContentItem> queryResult = new QueryResult<ContentItem>();
        queryResult.Items = result.Select(x => x.ToModel(schema)).ToList();
        queryResult.Offset = query.Skip;
        queryResult.Count = queryResult.Items.Count;
        queryResult.TotalCount = totalCount;

        return queryResult;
    }

    public async Task CreateAsync(ContentItem content)
    {
        if (content.Id == Guid.Empty)
        {
            content.Id = Guid.NewGuid();
        }

        DateTime now = DateTimeService.Current();

        content.CreatedAt = now;
        content.ModifiedAt = now;

        var items = GetMongoCollection(content.Schema.Name);

        content.Validate();

        MongoContentItem mongo = content.ToMongo();

        await items.InsertOneAsync(mongo);

        content.Id = mongo.Id;

        var interceptors = Api.ServiceProvider.GetServices<IContentInterceptor>();

        //execute interceptors
        foreach (IContentInterceptor interceptor in interceptors)
        {
            await interceptor.OnCreatedAsync(content);
        }
    }

    public async Task UpdateAsync(ContentItem content)
    {
        ContentSchema schema = await GetSchemaAsync(content.Schema.Name);


        content.ApplySchema(schema);

        IMongoCollection<MongoContentItem> drafts = GetMongoCollection(content.Schema.Name);

        content.Validate();

        if (content.ValidationContext.State == ValidationState.Invalid)
        {
            throw new Exception("Invalid state");
        }

        //update all fields, version
        MongoContentItem result = await drafts.FindOneAndUpdateAsync(
                                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id),
                                    Builders<MongoContentItem>.Update
                                                                .Set(x => x.Fields, content.Fields.ToMongo())
                                                                .Set(x => x.ModifiedAt, DateTimeService.Current())
                                                                .Set(x => x.PublishedAt, null)
                                                                .Set(x => x.SchemaVersion, content.SchemaVersion)
                                                                .Inc(x => x.Version, 1));

        content = result.ToModel(content.Schema);

        var interceptors = Api.ServiceProvider.GetServices<IContentInterceptor>();

        //execute interceptors
        foreach (IContentInterceptor interceptor in interceptors)
        {
            await interceptor.OnUpdatedAsync(content);
        }
    }

    public async Task DeleteAsync(ContentItem content)
    {
        await UnpublishAsync(content);

        IMongoCollection<MongoContentItem> col = GetMongoCollection(content.Schema.Name, false);

        await col.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id));


        var interceptors = Api.ServiceProvider.GetServices<IContentInterceptor>();

        //execute interceptors
        foreach (IContentInterceptor interceptor in interceptors)
        {
            await interceptor.OnDeletedAsync(content);
        }
    }

    public async Task PublishAsync(ContentItem content)
    {
        IMongoCollection<MongoContentItem> drafts = GetMongoCollection(content.Schema.Name, false);
        IMongoCollection<MongoContentItem> published = GetMongoCollection(content.Schema.Name, true);

        //find contentitem
        MongoContentItem found = await drafts.AsQueryable().FirstOrDefaultAsync(x => x.Id == content.Id);

        if (found == null)
        {
            throw new Exception($"Content item not found: {content.Schema.Name}/{content.Id}");
        }

        //add contentitem to published collection
        await published.ReplaceOneAsync(
                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id), found, new ReplaceOptions() { IsUpsert = true });

        //update publish date
        await drafts.UpdateOneAsync(
                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id),
                    Builders<MongoContentItem>.Update.Set(x => x.PublishedAt, DateTimeService.Current()));

        //refresh contentitem
        content = await GetContentAsync(content.Schema.Name, content.Id);

        if (content == null)
        {
            throw new Exception("ContentItem not found.");
        }

        var interceptors = Api.ServiceProvider.GetServices<IContentInterceptor>();

        //execute interceptors
        foreach (IContentInterceptor interceptor in interceptors)
        {
            try
            {
                await interceptor.OnPublishedAsync(content);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Publish interceptor failed.");
            }
        }

        Logger.LogInformation($"Content was published: {content.Schema.Name}/{content.Id}");
    }

    public async Task UnpublishAsync(ContentItem content)
    {
        IMongoCollection<MongoContentItem> drafts = GetMongoCollection(content.Schema.Name, false);
        IMongoCollection<MongoContentItem> published = GetMongoCollection(content.Schema.Name, true);

        //delete published contentitem
        await published.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id));

        //update publish date
        await drafts.UpdateOneAsync(
                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id),
                    Builders<MongoContentItem>.Update.Set(x => x.PublishedAt, null));

        content = await GetContentAsync(content.Schema.Name, content.Id);

        var interceptors = Api.ServiceProvider.GetServices<IContentInterceptor>();

        //execute interceptors
        foreach (IContentInterceptor interceptor in interceptors)
        {
            try
            {
                await interceptor.OnUnpublishedAsync(content);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unpublish interceptor failed.");
            }
        }
    }

    public async Task PublishQueryAsync(ContentQuery query)
    {
        BackgroundTaskService.Start(
            $"Publish all {query.Schema}", 
            query,
            static async ctx => await InternalPublishUnpublishAsync(true, ctx));
    }

    public async Task UnpublishQueryAsync(ContentQuery query)
    {
        BackgroundTaskService.Start(
            $"Unpublish all {query.Schema}",
            query,
            static async ctx => await InternalPublishUnpublishAsync(false, ctx));
    }

    private static async Task InternalPublishUnpublishAsync(bool publish, BackgroundTaskContext<ContentQuery> ctx)
    {
        IContentStorage contentStorage = ctx.ServiceProvider.GetRequiredService<IContentStorage>();

        int pageSize = 50;

        ctx.Input.Skip = 0;
        ctx.Input.Top = pageSize;

        int counter = 0;
        int counterSucceed = 0;
        int counterFailed = 0;

        while (true)
        {
            QueryResult<ContentItem> result = await contentStorage.QueryAsync(ctx.Input);

            if (result.Items.Count == 0)
            {
                break;
            }

            foreach (ContentItem contentItem in result.Items)
            {
                await ctx.UpdateStatusAsync(contentItem.ToString(), counter, result.TotalCount);

                try
                {
                    if (publish)
                    {
                        await contentStorage.PublishAsync(contentItem);
                    }
                    else
                    {
                        await contentStorage.UnpublishAsync(contentItem);
                    }

                    counterSucceed++;
                }
                catch
                {
                    counterFailed++;
                }
                finally
                {
                    counter++;
                }
            }

            ctx.Input.Skip += pageSize;
        }

        await ctx.UpdateStatusAsync($"Succeed: {counterSucceed} / Failed: {counterFailed}", progressValue: counter);
    }
}
