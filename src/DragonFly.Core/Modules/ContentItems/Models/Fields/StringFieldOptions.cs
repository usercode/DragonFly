﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class StringFieldOptions : SingleValueFieldOptions<string?>
{
    public const int DefaultMaxLength = 2048;

    public StringFieldOptions()
    {
        MinLength = 0;
        MaxLength = DefaultMaxLength;
    }

    /// <summary>
    /// MinLength
    /// </summary>
    public int? MinLength { get; set; }

    /// <summary>
    /// MaxLength
    /// </summary>
    public int? MaxLength { get; set; }

    public override ContentField CreateContentField()
    {
        return new StringField(DefaultValue);
    }
}
