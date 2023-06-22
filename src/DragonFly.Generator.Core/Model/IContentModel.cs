// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IContentModel
/// </summary>
public interface IContentModel
{
    /// <summary>
    /// Id of the content item.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Returns the internal content item.
    /// </summary>
    ContentItem GetContentItem();

    /// <summary>
    /// Metadata
    /// </summary>
    static abstract IContentMetadata Metadata { get; }
}

