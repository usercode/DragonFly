// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

/// <summary>
/// IContentStorage
/// </summary>
public interface IContentStorage
{
    Task<QueryResult<ContentItem>> QueryAsync(ContentQuery query);
        
    Task<ContentItem?> GetContentAsync(string schema, Guid id);

    Task CreateAsync(ContentItem contentItem);

    Task UpdateAsync(ContentItem entity);

    Task DeleteAsync(string schema, Guid id);

    Task PublishQueryAsync(ContentQuery query);

    Task UnpublishQueryAsync(ContentQuery query);

    Task PublishAsync(string schema, Guid id);

    Task UnpublishAsync(string schema, Guid id);
}
