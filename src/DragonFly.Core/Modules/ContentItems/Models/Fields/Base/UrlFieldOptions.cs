﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// UrlFieldOptions
/// </summary>
public class UrlFieldOptions : FieldOptions
{
    public UrlFieldOptions()
    {
    }

    /// <summary>
    /// UrlType
    /// </summary>
    public UrlType UrlType { get; set; } = UrlType.Absolute;

    public override ContentField CreateContentField()
    {
        return new UrlField();
    }
}
