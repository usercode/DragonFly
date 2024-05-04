// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly;

/// <summary>
/// Defines storage engine for content versions.
/// </summary>
public interface IContentVersionStorage
{
    /// <summary>
    /// GetContentVersionsAsync
    /// </summary>
    Task<Result<IEnumerable<ContentVersionEntry>>> GetContentVersionsAsync(string schema, Guid id);

    /// <summary>
    /// GetContentByVersionAsync
    /// </summary>
    Task<Result<ContentItem?>> GetContentByVersionAsync(string schema, Guid id);
}
