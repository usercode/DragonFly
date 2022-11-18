// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly.Storage;

/// <summary>
/// IContentStorage
/// </summary>
public interface IContentStorage
{
    Task<QueryResult<ContentItem>> QueryAsync(ContentItemQuery query);
        
    Task<ContentItem?> GetContentAsync(string schema, Guid id);

    Task CreateAsync(ContentItem contentItem);

    Task UpdateAsync(ContentItem entity);

    Task DeleteAsync(string schema, Guid id);

    Task PublishQueryAsync(ContentItemQuery query);

    Task UnpublishQueryAsync(ContentItemQuery query);

    Task PublishAsync(string schema, Guid id);

    Task UnpublishAsync(string schema, Guid id);
}
