// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Results;

namespace DragonFly;

/// <summary>
/// Defines storage engine for content items.
/// </summary>
public interface IContentStorage
{    
    Task<Result<ContentItem?>> GetContentAsync(string schema, Guid id);

    Task<Result> CreateAsync(ContentItem content);

    Task<Result> UpdateAsync(ContentItem content);

    Task<Result> DeleteAsync(ContentItem content);

    Task<Result> PublishAsync(ContentItem content);

    Task<Result> UnpublishAsync(ContentItem content);

    Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery query);

    Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query);

    Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query);
}
