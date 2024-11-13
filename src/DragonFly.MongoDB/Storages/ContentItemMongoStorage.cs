// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Microsoft.Extensions.DependencyInjection;
using DragonFly.AspNetCore;
using SmartResults;
using DragonFly.MongoDB.Storages;
using Microsoft.Extensions.Options;

namespace DragonFly.MongoDB;

/// <summary>
/// ContentItemMongoStorage
/// </summary>
public class ContentItemMongoStorage : MongoStorage, IContentStorage
{
    public ContentItemMongoStorage(
                        MongoClient client,
                        IOptions<DragonFlyOptions> dragonFlyOptions,
                        ISchemaStorage schemaStorage,
                        IDateTimeService dateTimeService,
                        IBackgroundTaskManager backgroundTaskManager,
                        IEnumerable<IContentInterceptor> contentInterceptors,
                        ILogger<ContentItemMongoStorage> logger)
        : base(client)
    {
        DragonFlyOptions = dragonFlyOptions.Value;
        SchemaStorage = schemaStorage;
        DateTimeService = dateTimeService;
        BackgroundTaskManager = backgroundTaskManager;
        ContentInterceptors = contentInterceptors;
        Logger = logger;
    }

    /// <summary>
    /// DragonFlyOptions
    /// </summary>
    private DragonFlyOptions DragonFlyOptions { get; }

    /// <summary>
    /// SchemaStorage
    /// </summary>
    private ISchemaStorage SchemaStorage { get; }

    /// <summary>
    /// DateTimeService
    /// </summary>
    private IDateTimeService DateTimeService { get; }

    /// <summary>
    /// BackgroundTaskManager
    /// </summary>
    private IBackgroundTaskManager BackgroundTaskManager { get; }

    /// <summary>
    /// ContentInterceptor
    /// </summary>
    private IEnumerable<IContentInterceptor> ContentInterceptors { get; }

    /// <summary>
    /// Logger
    /// </summary>
    private ILogger<ContentItemMongoStorage> Logger { get; }

    public async Task<Result<ContentItem?>> GetContentAsync(string schema, Guid id)
    {
        ContentSchema? contentSchema = await SchemaStorage.GetSchemaAsync(schema);

        if (contentSchema == null)
        {
            return Result.Failed<ContentItem?>($"The schema '{schema}' doesn't exist.");
        }

        IMongoCollection<MongoContentItem> collection = Client.Database.GetContentCollection(schema);

        MongoContentItem? result = collection.AsQueryable().FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            return Result.Ok<ContentItem?>();
        }

        return result.ToModel(contentSchema);
    }

    public async Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery query)
    {
        ContentSchema? schema = await SchemaStorage.GetSchemaAsync(query.Schema);

        ArgumentNullException.ThrowIfNull(schema);

        IMongoCollection<MongoContentItem> collection = Client.Database.GetContentCollection(schema.Name, query.Published);

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
        if (query.Reference != null)
        {            
            filters.Add(Builders<MongoContentItem>.Filter.ElemMatch(x => x.ReferencedTo,
                                                            Builders<MongoReferencedContent>.Filter.And(
                                                                Builders<MongoReferencedContent>.Filter.Eq(x => x.Schema, query.Reference.Value.Schema),
                                                                Builders<MongoReferencedContent>.Filter.Eq(x => x.Id2, query.Reference.Value.Id)
                                                            )));
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

        List<MongoContentItem> result = await cursor.ToListAsync();

        QueryResult<ContentItem> queryResult = new QueryResult<ContentItem>();
        queryResult.Items = result.Select(x => x.ToModel(schema)).ToList();
        queryResult.Offset = query.Skip;
        queryResult.Count = queryResult.Items.Count;
        queryResult.TotalCount = totalCount;

        return queryResult;
    }

    public async Task<Result> CreateAsync(ContentItem content)
    {
        ArgumentNullException.ThrowIfNull(content);

        if (content.Id == Guid.Empty)
        {
            content.Id = Guid.NewGuid();
        }

        DateTime now = DateTimeService.Current();

        content.CreatedAt = now;
        content.ModifiedAt = now;

        var items = Client.Database.GetContentCollection(content.Schema.Name);

        ContentSchema? schema = await SchemaStorage.GetSchemaAsync(content.Schema.Name);

        content.ApplySchema(schema);
        content.Validate();

        MongoContentItem mongo = content.ToMongo();

        mongo.ReferencedTo = content.GetReferencedContent()
                                   .Select(x => new MongoReferencedContent() { Id2 = x.Id, Schema = x.Schema })
                                   .ToList();

        await items.InsertOneAsync(mongo);

        content.Id = mongo.Id;

        //execute interceptors
        foreach (IContentInterceptor interceptor in ContentInterceptors)
        {
            await interceptor.OnCreatedAsync(content);
        }

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(ContentItem content)
    {
        ContentSchema? schema = await SchemaStorage.GetSchemaAsync(content.Schema.Name);

        ArgumentNullException.ThrowIfNull(schema);

        content.ApplySchema(schema);
        content.Validate();

        if (content.ValidationState.Result == ValidationResult.Invalid)
        {
            return Result.Failed(ContentErrors.InvalidState);
        }

        IMongoCollection<MongoContentItem> drafted = Client.Database.GetContentCollection(content.Schema.Name);

        //versioning
        if (DragonFlyOptions.Versioning != ContentVersionKind.None)
        {
            IMongoCollection<MongoContentVersion> versioning = Client.Database.GetContentVersionCollection(content.Schema.Name);

            MongoContentItem draftItem = await drafted.AsQueryable().FirstOrDefaultAsync(x => x.Id == content.Id);

            if (DragonFlyOptions.Versioning == ContentVersionKind.All ||
                (DragonFlyOptions.Versioning == ContentVersionKind.PublishedOnly && draftItem.PublishedAt != null))
            {
                await versioning.InsertOneAsync(new MongoContentVersion() { Content = draftItem });
            }
        }

        var referencedTo = content.GetReferencedContent()
                                    .Select(x => new MongoReferencedContent() { Id2 = x.Id, Schema = x.Schema })
                                    .ToList();

        //update all fields, version
        MongoContentItem mongoContentItem = await drafted.FindOneAndUpdateAsync(
                                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id),
                                    Builders<MongoContentItem>.Update
                                                                .Set(x => x.Fields, content.Fields.ToMongo())
                                                                .Set(x => x.ReferencedTo, referencedTo)
                                                                .Set(x => x.ModifiedAt, DateTimeService.Current())
                                                                .Set(x => x.PublishedAt, null)
                                                                .Set(x => x.SchemaVersion, content.SchemaVersion)
                                                                .Inc(x => x.Version, 1),
                                    new FindOneAndUpdateOptions<MongoContentItem>() { ReturnDocument = ReturnDocument.After });

        if (mongoContentItem == null)
        {
            return Result.Failed<bool>(ContentErrors.NotFound);
        }

        content = mongoContentItem.ToModel(schema);

        //execute interceptors
        foreach (IContentInterceptor interceptor in ContentInterceptors)
        {
            await interceptor.OnUpdatedAsync(content);
        }

        return Result.Ok();
    }

    public async Task<Result<bool>> DeleteAsync(string schema, Guid id)
    {
        var result = await GetContentAsync(schema, id);

        if (result.IsFailed)
        {
            return Result.Failed<bool>(result.Error);
        }

        if (result.Value == null)
        {
            return Result.Ok(false);
        }

        ContentItem content = result.Value;

        await UnpublishAsync(schema, id);

        IMongoCollection<MongoContentItem> col = Client.Database.GetContentCollection(content.Schema.Name);

        await col.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id));

        //execute interceptors
        foreach (IContentInterceptor interceptor in ContentInterceptors)
        {
            await interceptor.OnDeletedAsync(content);
        }

        return Result.Ok(true);
    }

    public async Task<Result<bool>> PublishAsync(string schema, Guid id)
    {
        var result = await GetContentAsync(schema, id);

        if (result.IsFailed)
        {
            return Result.Failed<bool>(result.Error);
        }

        if (result.Value == null)
        {
            return Result.Failed<bool>(ContentErrors.NotFound);
        }

        ContentItem content = result.Value;

        IMongoCollection<MongoContentItem> drafted = Client.Database.GetContentCollection(content.Schema.Name, false);
        IMongoCollection<MongoContentItem> published = Client.Database.GetContentCollection(content.Schema.Name, true);

        //find content and updated published date
        MongoContentItem found = drafted.FindOneAndUpdate(
                                   Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id),
                                   Builders<MongoContentItem>.Update.Set(x => x.PublishedAt, DateTimeService.Current()),
                                   new FindOneAndUpdateOptions<MongoContentItem>() { ReturnDocument = ReturnDocument.After });

        if (found == null)
        {
            return Result.Failed<bool>(ContentErrors.NotFound);
        }

        //add content to published collection
        await published.ReplaceOneAsync(
                                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, found.Id), 
                                    found, 
                                    new ReplaceOptions() { IsUpsert = true });

        content = found.ToModel(content.Schema);

        //execute interceptors
        foreach (IContentInterceptor interceptor in ContentInterceptors)
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
        
        return Result.Ok(true);
    }

    public async Task<Result<bool>> UnpublishAsync(string schema, Guid id)
    {
        var result = await GetContentAsync(schema, id);

        if (result.IsFailed)
        {
            return Result.Failed<bool>(result.Error);
        }

        if (result.Value == null)
        {
            return Result.Failed<bool>(ContentErrors.NotFound);
        }

        ContentItem? content = result.Value;

        IMongoCollection<MongoContentItem> drafted = Client.Database.GetContentCollection(content.Schema.Name, false);
        IMongoCollection<MongoContentItem> published = Client.Database.GetContentCollection(content.Schema.Name, true);

        //delete published content
        await published.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id));

        //update publish date
        MongoContentItem found = await drafted.FindOneAndUpdateAsync(
                                                        Builders<MongoContentItem>.Filter.Eq(x => x.Id, content.Id),
                                                        Builders<MongoContentItem>.Update.Set(x => x.PublishedAt, null),
                                                        new FindOneAndUpdateOptions<MongoContentItem>() { ReturnDocument = ReturnDocument.After });

        if (found == null)
        {
            return Result.Failed<bool>(ContentErrors.NotFound);
        }

        content = found.ToModel(content.Schema);

        //execute interceptors
        foreach (IContentInterceptor interceptor in ContentInterceptors)
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

        return Result.Ok(true);
    }

    public Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query)
    {
        BackgroundTask task = BackgroundTaskManager.Start(
                                                        $"Publish all {query.Schema}",
                                                        query,
                                                        static async ctx =>
                                                        {
                                                            IContentStorage contentStorage = ctx.ServiceProvider.GetRequiredService<IContentStorage>();

                                                            await ctx.ProcessQueryAsync(contentStorage.QueryAsync, async x => await contentStorage.PublishAsync(x.Schema.Name, x.Id));
                                                        });

        return Task.FromResult<Result<BackgroundTaskInfo>>((BackgroundTaskInfo)task);
    }

    public Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query)
    {
        BackgroundTask task = BackgroundTaskManager.Start(
                                                        $"Unpublish all {query.Schema}",
                                                        query,
                                                        static async ctx =>
                                                        {
                                                            IContentStorage contentStorage = ctx.ServiceProvider.GetRequiredService<IContentStorage>();

                                                            await ctx.ProcessQueryAsync(contentStorage.QueryAsync, async x => await contentStorage.UnpublishAsync(x.Schema.Name, x.Id));
                                                        });

        return Task.FromResult<Result<BackgroundTaskInfo>>((BackgroundTaskInfo)task);
    }

    public async Task<Result<ContentReferenceIndex>> GetReferencedByAsync(string schema, Guid id)
    {
        var result = await SchemaStorage.QuerySchemasAsync();

        if (result.IsFailed)
        {
            return Result.Failed<ContentReferenceIndex>(result.Error);
        }

        List<ContentReferenceIndexEntry> entries = new List<ContentReferenceIndexEntry>();

        foreach (ContentSchema item in result.Value.Items)
        {
            IMongoCollection<MongoContentItem> collection = Client.Database.GetContentCollection(item.Name, false);

            var filter = Builders<MongoContentItem>.Filter.ElemMatch(x => x.ReferencedTo,
                                                            Builders<MongoReferencedContent>.Filter.And(
                                                                Builders<MongoReferencedContent>.Filter.Eq(x => x.Schema, schema),
                                                                Builders<MongoReferencedContent>.Filter.Eq(x => x.Id2, id)));

            long totalCount = await collection.CountDocumentsAsync(filter);

            if (totalCount > 0)
            {
                entries.Add(new ContentReferenceIndexEntry() { Schema = item.Name, Count = totalCount });
            }
        }

        return new ContentReferenceIndex() { Entries = entries };
    }

    public async Task<Result> RebuildDatabaseAsync()
    {
        var result = await SchemaStorage.QuerySchemasAsync();

        foreach (ContentSchema s in result.Value.Items)
        {
            BackgroundTaskManager.Start($"Rebuild {s.Name}", new ContentQuery(s.Name) { Published = false },
                async x =>
                {
                    IContentStorage contentStorage = x.ServiceProvider.GetRequiredService<IContentStorage>();

                    await x.ProcessQueryAsync(contentStorage.QueryAsync, async i => await contentStorage.UpdateAsync(i));
                }
            );
        }

        return Result.Ok();
    }
}
