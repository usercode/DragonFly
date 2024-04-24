// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Results;

namespace DragonFly;

/// <summary>
/// ISchemaStorage
/// </summary>
public interface ISchemaStorage
{
    //Schema
    Task<Result<ContentSchema?>> GetSchemaAsync(Guid id);

    Task<Result<ContentSchema?>> GetSchemaAsync(string name);

    Task<Result> CreateAsync(ContentSchema schema);

    Task<Result> UpdateAsync(ContentSchema schema);

    Task<Result> DeleteAsync(ContentSchema schema);

    Task<Result<QueryResult<ContentSchema>>> QuerySchemasAsync();
}
