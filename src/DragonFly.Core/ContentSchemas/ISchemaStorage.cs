// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IMongoStore
/// </summary>
public interface ISchemaStorage
{
    //Schema
    Task<ContentSchema?> GetSchemaAsync(Guid id);

    Task<ContentSchema?> GetSchemaAsync(string name);

    Task CreateAsync(ContentSchema contentType);

    Task UpdateAsync(ContentSchema entity);

    Task DeleteAsync(ContentSchema entity);

    //IList<ContentSchema> QueryContentSchemas()
    //{
    //    return QueryContentSchemas(new QueryParameters());
    //}

    Task<QueryResult<ContentSchema>> QuerySchemasAsync();
}
