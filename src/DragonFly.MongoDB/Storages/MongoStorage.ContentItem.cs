// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.AspNetCore;
using Results;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IContentStorage
{
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

    public IMongoCollection<MongoContentVersion> GetMongoCollectionVersioning(string type)
    {
        string name = $"ContentItems_{type}_Version";

        if (ContentVersioning.TryGetValue(name, out IMongoCollection<MongoContentVersion>? items) == false)
        {
            items = Database.GetCollection<MongoContentVersion>(name);

            ContentVersioning.Add(name, items);
        }

        return items;
    }

    public async Task<Result<ContentItem>> GetContentAsync(string schema, Guid id)
    {
        ContentSchema? contentSchema = await GetSchemaAsync(schema);

        if (contentSchema == null)
        {
            throw new Exception($"The schema '{schema}' doesn't exist.");
        }

        IMongoCollection<MongoContentItem> collection = GetMongoCollection(schema);

        MongoContentItem? result = collection.AsQueryable().FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            return Result.Ok();
        }

        return result.ToModel(contentSchema);
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery query)
    {
        ContentSchema? schema = await GetSchemaAsync(query.Schema);

        ArgumentNullException.ThrowIfNull(schema);

        IMongoCollection<MongoContentItem> collection = GetMongoCollection(schema.Name, query.Published);

        List<FilterDefinition<MongoContentItem>> filters = new List<FilterDefinition<MongoContentItem>>();

        //filter all fields
        if (string.IsNullOrEmpty(query.Pattern) == false)
        {
            List<FilterDefinition<MongoContentItem>> patternQuery = new List<FilterDefinition<MongoContentItem>>();

            foreach (string field in schema.Fields
                                    .Where(x => x.Value.FieldType == FieldManager.Default.GetFieldName<StringField>())
                                    .Select(x => x.Key))
            {
                patternQuery.Add(Builders<MongoContentItem>.Filter.Regex($"{nameof(MongoContentItem.Fields)}.{field}", new BsonRegularExpression(query.Pattern, "i")));
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
            Limit = query.Take,
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
            string assetFieldName = FieldManager.Default.GetFieldName<AssetField>();

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

        long totalCount = await collection.EstimatedDocumentCountAsync();

        IList<MongoContentItem> result = await cursor.ToListAsync();

        QueryResult<ContentItem> queryResult = new QueryResult<ContentItem>();
        queryResult.Items = result.Select(x => x.ToModel(schema)).ToList();
        queryResult.Offset = query.Skip;
        queryResult.Count = queryResult.Items.Count;
        queryResult.TotalCount = totalCount;

        return queryResult;
    }

    public async Task<Result> CreateAsync(ContentItem content)
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

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(ContentItem content)
    {
        ContentSchema? schema = await GetSchemaAsync(content.Schema.Name);

        content.ApplySchema(schema);
        content.Validate();        

        if (content.ValidationState.Result == ValidationResult.Invalid)
        {
            throw new Exception("Invalid state");
        }

        IMongoCollection<MongoContentItem> drafted = GetMongoCollection(content.Schema.Name);

        //versioning
        if (DragonFlyOptions.Versioning != ContentVersionKind.None)
        {
            IMongoCollection<MongoContentVersion> versioning = GetMongoCollectionVersioning(content.Schema.Name);

            MongoContentItem draftItem = await drafted.AsQueryable().FirstOrDefaultAsync(x => x.Id == content.Id);

            if (DragonFlyOptions.Versioning == ContentVersionKind.All ||
                (DragonFlyOptions.Versioning == ContentVersionKind.PublishedOnly && draftItem.PublishedAt != null))
            {
                await versioning.InsertOneAsync(new MongoContentVersion() { Content = draftItem });
            }
        }

        //update all fields, version
        MongoContentItem result = await drafted.FindOneAndUpdateAsync(
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

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(ContentItem content)
    {
        await UnpublishAsync(content);

        IMongoCollection<MongoContentItem> col = GetMongoCollection(content.Schema.Name);

        await col.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id));

        var interceptors = Api.ServiceProvider.GetServices<IContentInterceptor>();

        //execute interceptors
        foreach (IContentInterceptor interceptor in interceptors)
        {
            await interceptor.OnDeletedAsync(content);
        }

        return Result.Ok();
    }

    public async Task<Result> PublishAsync(ContentItem content)
    {
        IMongoCollection<MongoContentItem> drafted = GetMongoCollection(content.Schema.Name, false);
        IMongoCollection<MongoContentItem> published = GetMongoCollection(content.Schema.Name, true);

        //find content and updated published date
        MongoContentItem found = drafted.FindOneAndUpdate(
                                   Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id),
                                   Builders<MongoContentItem>.Update.Set(x => x.PublishedAt, DateTimeService.Current()),
                                   new FindOneAndUpdateOptions<MongoContentItem>() { ReturnDocument = ReturnDocument.After });

        if (found == null)
        {
            throw new Exception($"Content item not found: {content.Schema.Name}/{content.Id}");
        }

        //add content to published collection
        await published.ReplaceOneAsync(
                                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, found.Id), 
                                    found, 
                                    new ReplaceOptions() { IsUpsert = true });

        //refresh content
        content = await GetContentAsync(content.Schema.Name, content.Id);

        if (content == null)
        {
            throw new Exception($"Content item not found: {content.Schema.Name}/{content.Id}");
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

        return Result.Ok();
    }

    public async Task<Result> UnpublishAsync(ContentItem content)
    {
        IMongoCollection<MongoContentItem> drafted = GetMongoCollection(content.Schema.Name, false);
        IMongoCollection<MongoContentItem> published = GetMongoCollection(content.Schema.Name, true);

        //delete published content
        await published.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id));

        //update publish date
        await drafted.UpdateOneAsync(
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

        return Result.Ok();
    }

    public Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        BackgroundTask task = BackgroundTaskService.Start(
                                                        $"Publish all {query.Schema}",
                                                        query,
                                                        static async ctx =>
                                                        {
                                                            IContentStorage contentStorage = ctx.ServiceProvider.GetRequiredService<IContentStorage>();

                                                            await ctx.ProcessQueryAsync(contentStorage.QueryAsync, contentStorage.PublishAsync);
                                                        });

        return Task.FromResult<Result<BackgroundTaskInfo>>((BackgroundTaskInfo)task);
    }

    public Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query)
    {
        BackgroundTask task = BackgroundTaskService.Start(
                                                        $"Unpublish all {query.Schema}",
                                                        query,
                                                        static async ctx =>
                                                        {
                                                            IContentStorage contentStorage = ctx.ServiceProvider.GetRequiredService<IContentStorage>();

                                                            await ctx.ProcessQueryAsync(contentStorage.QueryAsync, contentStorage.UnpublishAsync);
                                                        });

        return Task.FromResult<Result<BackgroundTaskInfo>>((BackgroundTaskInfo)task);
    }
}
