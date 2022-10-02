// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
