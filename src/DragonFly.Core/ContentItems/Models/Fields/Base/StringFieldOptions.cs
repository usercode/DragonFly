﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public class StringFieldOptions : ContentFieldOptions
{
    public const int DefaultMaxLength = 2048;

    public StringFieldOptions()
    {
        MinLength = 0;
        MaxLength = DefaultMaxLength;
    }

    /// <summary>
    /// DefaultValue
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// MinLength
    /// </summary>
    public int? MinLength { get; set; }

    /// <summary>
    /// MaxLength
    /// </summary>
    public int? MaxLength { get; set; }

    public override IContentField CreateContentField()
    {
        return new StringField(DefaultValue);
    }
}
