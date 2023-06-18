// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Generator;

internal class ContentItemProperty
{
    public string PropertyName { get; set; }
    public string PropertyType { get; set; }
    public bool IsSingleValue { get; set; }
    public string FieldName { get; set; }
}
