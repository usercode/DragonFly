// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class IgnorePropertyAttribute : Attribute
{
    public IgnorePropertyAttribute(string property)
    {
        Property = property;
    }

    public string Property { get; }
}
