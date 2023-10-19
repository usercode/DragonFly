// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ISchemaStorage
/// </summary>
public interface ISchemaStorage
{
    //Schema
    Task<ContentSchema?> GetSchemaAsync(Guid id);

    Task<ContentSchema?> GetSchemaAsync(string name);

    Task CreateAsync(ContentSchema schema);

    Task UpdateAsync(ContentSchema schema);

    Task DeleteAsync(ContentSchema schema);

    Task<QueryResult<ContentSchema>> QuerySchemasAsync();
}
