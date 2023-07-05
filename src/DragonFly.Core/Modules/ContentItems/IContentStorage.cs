// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IContentStorage
/// </summary>
public interface IContentStorage
{    
    Task<ContentItem?> GetContentAsync(string schema, Guid id);

    Task CreateAsync(ContentItem content);

    Task UpdateAsync(ContentItem content);

    Task DeleteAsync(ContentItem content);    

    Task PublishAsync(ContentItem content);

    Task UnpublishAsync(ContentItem content);

    Task<QueryResult<ContentItem>> QueryAsync(ContentQuery query);

    Task<IBackgroundTaskInfo> PublishQueryAsync(ContentQuery query);

    Task<IBackgroundTaskInfo> UnpublishQueryAsync(ContentQuery query);
}
