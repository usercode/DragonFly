// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ArrayFieldItem
/// </summary>
public class ArrayFieldItem : IContentElement
{
    public ArrayFieldItem()
    {
        Fields = new ContentFields();
    }

    /// <summary>
    /// Fields
    /// </summary>
    public ContentFields Fields { get; set; }
}
