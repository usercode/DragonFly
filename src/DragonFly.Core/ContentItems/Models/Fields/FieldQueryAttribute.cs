// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldQueryAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class FieldQueryAttribute : Attribute
{
    public FieldQueryAttribute(Type queryType)
    {
        QueryType = queryType;
    }

    /// <summary>
    /// QueryType
    /// </summary>
    public Type QueryType { get; }
}
