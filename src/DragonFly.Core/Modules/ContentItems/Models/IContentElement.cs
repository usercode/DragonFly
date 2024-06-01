// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// It defines an element which owns fields.<br/>
/// It's implemented by <see cref="ContentItem"/>, <see cref="ContentComponent"/> and <see cref="ArrayFieldItem"/>.
/// </summary>
public interface IContentElement
{
    /// <summary>
    /// Fields
    /// </summary>
    ContentFields Fields { get; }
}
