// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly;

/// <summary>
/// Defines storage engine for content items.
/// </summary>
public interface IContentStorage
{    
    Task<Result<ContentItem?>> GetContentAsync(string schema, Guid id);

    Task<Result> CreateAsync(ContentItem content);

    Task<Result> UpdateAsync(ContentItem content);

    Task<Result<bool>> DeleteAsync(string schema, Guid id);

    Task<Result<bool>> PublishAsync(string schema, Guid id);

    Task<Result<bool>> UnpublishAsync(string schema, Guid id);

    Task<Result<QueryResult<ContentItem>>> QueryAsync(ContentQuery query);

    Task<Result<BackgroundTaskInfo>> PublishQueryAsync(ContentQuery query);

    Task<Result<BackgroundTaskInfo>> UnpublishQueryAsync(ContentQuery query);

    Task<Result<ContentReferenceIndex>> GetReferencedByAsync(string schema, Guid id);

    Task<Result> RebuildDatabaseAsync();
}
