// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

internal class ContentItemProperty
{
    public string PropertyName { get; set; }
    public string PropertyType { get; set; }
    public bool IsSingleValue { get; set; }
    public string AttributeName { get; set; }
    public string AttributeParameters { get; set; }
}
