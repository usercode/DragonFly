// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Core.ContentItems.Models.Validations;
using DragonFly.MongoDB.Query;
using DragonFly.Query;
using DragonFly.Storage;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoStore
/// </summary>
public partial class MongoStorage : IContentStorage
{
    public async Task<ContentItem> GetContentAsync(string schema, Guid id)
    {
        ContentSchema contentSchema = await GetSchemaAsync(schema);
        IMongoCollection<MongoContentItem> collection = GetMongoCollection(schema);

        MongoContentItem? result = collection.AsQueryable().FirstOrDefault(x => x.Id == id);

        if (result == null)
        {
            throw new Exception($"ContentItem '{schema}/{id}' not found");
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

    public async Task<QueryResult<ContentItem>> QueryAsync(ContentItemQuery query)
    {
        ContentSchema schema = await GetSchemaAsync(query.Schema);
        IMongoCollection<MongoContentItem> collection = GetMongoCollection(schema.Name, query.Published);

        List<FilterDefinition<MongoContentItem>> filters = new List<FilterDefinition<MongoContentItem>>();

        //filter all fields
        if (string.IsNullOrEmpty(query.SearchPattern) == false)
        {
            List<FilterDefinition<MongoContentItem>> patternQuery = new List<FilterDefinition<MongoContentItem>>();

            foreach (string field in schema.Fields
                                    .Where(x=> x.Value.FieldType == ContentFieldManager.Default.GetContentFieldName<StringField>())
                                    .Select(x=> x.Key))
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

        var findOptions = new FindOptions<MongoContentItem>()
        {
            Limit = query.Top,
            Skip = query.Skip
        };

        //use default order?
        if (query.OrderFields.Any() == false)
        {
            query.OrderFields = schema.OrderFields.ToList();
        }

        //order
        foreach (FieldOrder orderField in query.OrderFields)
        {
            if (orderField.Asc)
            {
                findOptions.Sort = Builders<MongoContentItem>.Sort.Ascending(orderField.Name);
            }
            else
            {
                findOptions.Sort = Builders<MongoContentItem>.Sort.Descending(orderField.Name);
            }
        }

        //projection
        if (query.IncludeListFieldsOnly && schema.ListFields.Any())
        {
            var p = Builders<MongoContentItem>.Projection.Include(x => x.Id);

            foreach (string field in schema.ListFields)
            {
                p = p.Include($"{nameof(MongoContentItem.Fields)}.{field}");
            }

            findOptions.Projection = p;
        }

        //FieldQuery
        FieldQueryActionContext converterContext = new FieldQueryActionContext();

        foreach (FieldQuery f in query.Fields)
        {
            IFieldQueryAction converter = MongoQueryManager.Default.GetByType(f.GetType());

            converter.Apply(f, converterContext);
        }

        filters.AddRange(converterContext.Filters);

        //bundle filter definitions
        FilterDefinition<MongoContentItem> q;

        if (filters.Count >= 2)
        {
            q = Builders<MongoContentItem>.Filter.And(filters);
        }
        else if (filters.Count == 1)
        {
            q = filters.First();
        }
        else
        {
            q = Builders<MongoContentItem>.Filter.Empty;
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

    public async Task CreateAsync(ContentItem contentItem)
    {
        if (contentItem.Id == Guid.Empty)
        {
            contentItem.Id = Guid.NewGuid();
        }

        DateTime now = DateTimeService.Current();

        contentItem.CreatedAt = now;
        contentItem.ModifiedAt = now;

        var items = GetMongoCollection(contentItem.Schema.Name);

        contentItem.Validate();

        MongoContentItem mongo = contentItem.ToMongo();

        await items.InsertOneAsync(mongo);

        contentItem.Id = mongo.Id;
    }

    public async Task UpdateAsync(ContentItem contentItem)
    {
        contentItem.ApplySchema();

        IMongoCollection<MongoContentItem> drafts = GetMongoCollection(contentItem.Schema.Name);

        ValidationContext validations = contentItem.Validate();

        if (validations.Errors.Any())
        {
            throw new Exception();
        }

        //update all fields, version
        MongoContentItem result = await drafts.FindOneAndUpdateAsync(
                                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, contentItem.Id),
                                    Builders<MongoContentItem>.Update
                                                                .Set(x => x.Fields, contentItem.Fields.ToMongo())
                                                                .Set(x => x.ModifiedAt, DateTimeService.Current())
                                                                .Set(x => x.PublishedAt, null)
                                                                .Set(x => x.SchemaVersion, contentItem.SchemaVersion)
                                                                .Inc(x => x.Version, 1));

        ContentItem content = result.ToModel(contentItem.Schema);

        //execute interceptors
        foreach (IContentInterceptor interceptor in Interceptors)
        {
            await interceptor.OnUpdatedAsync(this, content);
        }
    }

    public async Task DeleteAsync(string schema, Guid id)
    {
        ContentItem contentItem = await GetContentAsync(schema, id);

        await UnpublishAsync(schema, id);

        IMongoCollection<MongoContentItem> col = GetMongoCollection(schema, false);

        await col.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, id));

        //execute interceptors
        foreach (IContentInterceptor interceptor in Interceptors)
        {
            await interceptor.OnDeletedAsync(this, contentItem);
        }
    }

    public async Task PublishAsync(string schema, Guid id)
    {
        ContentItem contentItem = await GetContentAsync(schema, id);

        //execute interceptors
        foreach (IContentInterceptor interceptor in Interceptors)
        {
            await interceptor.OnPublishingAsync(this, contentItem);
        }

        IMongoCollection<MongoContentItem> drafts = GetMongoCollection(schema, false);
        IMongoCollection<MongoContentItem> published = GetMongoCollection(schema, true);

        //find contentitem
        MongoContentItem found = await drafts.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        if (found == null)
        {
            throw new Exception($"Content item not found: {schema}/{id}");
        }

        //add contentitem to published collection
        await published.ReplaceOneAsync(
                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, id), found, new ReplaceOptions() { IsUpsert = true });

        //update publish date
        await drafts.UpdateOneAsync(
                    Builders<MongoContentItem>.Filter.Eq(x=> x.Id, id), 
                    Builders<MongoContentItem>.Update.Set(x => x.PublishedAt, DateTimeService.Current()));

        //refresh contentitem
        contentItem = await GetContentAsync(schema, id);

        //execute interceptors
        foreach (IContentInterceptor interceptor in Interceptors)
        {
            try
            {
                await interceptor.OnPublishedAsync(this, contentItem);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Publish interceptor failed.");
            }
        }

        Logger.LogInformation($"Content was published: {schema}/{id}");
    }

    public async Task UnpublishAsync(string schema, Guid id)
    {
        IMongoCollection<MongoContentItem> drafts = GetMongoCollection(schema, false);
        IMongoCollection<MongoContentItem> published = GetMongoCollection(schema, true);

        //delete published contentitem
        await published.DeleteOneAsync(Builders<MongoContentItem>.Filter.Eq(x => x.Id, id));

        //update publish date
        await drafts.UpdateOneAsync(
                    Builders<MongoContentItem>.Filter.Eq(x => x.Id, id), 
                    Builders<MongoContentItem>.Update.Set(x => x.PublishedAt, null));

        ContentItem contentItem = await GetContentAsync(schema, id);

        //execute interceptors
        foreach (IContentInterceptor interceptor in Interceptors)
        {
            try
            {
                await interceptor.OnUnpublishedAsync(this, contentItem);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unpublish interceptor failed.");
            }
        }
    }

    public async Task PublishQueryAsync(ContentItemQuery query)
    {
        int pageSize = 50;

        query.Skip = 0;
        query.Top = pageSize;

        while (true)
        {
            var result = await QueryAsync(query);

            if (result.Items.Count == 0)
            {
                break;
            }

            foreach (ContentItem contentItem in result.Items)
            {
                await PublishAsync(contentItem.Schema.Name, contentItem.Id);
            }

            query.Skip += pageSize;
        }            
    }
}
