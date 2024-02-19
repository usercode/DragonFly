// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Defines storage engine for content versions.
/// </summary>
public interface IContentVersionStorage
{
    /// <summary>
    /// GetContentVersionsAsync
    /// </summary>
    Task<IEnumerable<ContentVersionEntry>> GetContentVersionsAsync(string schema, Guid id);

    /// <summary>
    /// GetContentByVersionAsync
    /// </summary>
    Task<ContentItem?> GetContentByVersionAsync(string schema, Guid id);
}
