// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IContentMetadata
/// </summary>
public interface IContentMetadata
{
    /// <summary>
    /// ModelName
    /// </summary>
    string ModelName { get; }

    /// <summary>
    /// Schema
    /// </summary>
    ContentSchema Schema { get; }

    /// <summary>
    /// Creates model with new content item.
    /// </summary>
    IContentModel CreateModel();

    /// <summary>
    /// Creates model for existing content item.
    /// </summary>
    IContentModel CreateModel(ContentItem contentItem);
    

}
