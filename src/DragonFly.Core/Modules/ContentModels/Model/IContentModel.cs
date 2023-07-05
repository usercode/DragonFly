// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// A model which decorates a content item.
/// </summary>
public interface IContentModel
{
    /// <summary>
    /// Id of the content item.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the internal content item.
    /// </summary>
    ContentItem GetContentItem();

    /// <summary>
    /// Schema
    /// </summary>
    static abstract ContentSchema Schema { get; }

    /// <summary>
    /// Creates model for existing content item.
    /// </summary>
    static abstract IContentModel Create(ContentItem contentItem);
}

