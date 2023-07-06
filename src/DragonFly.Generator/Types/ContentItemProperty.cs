// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.CodeAnalysis;

namespace DragonFly.Generator;

public class ContentItemProperty
{
    public string PropertyName { get; set; }
    public string AttributeParameters { get; set; }
    public ITypeSymbol AttributeTypeSymbol { get; set; }
    public ITypeSymbol PropertyTypeSymbol { get; set; }
}
