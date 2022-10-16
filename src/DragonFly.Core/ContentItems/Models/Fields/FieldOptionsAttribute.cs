// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldOptionsAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class FieldOptionsAttribute : Attribute
{
    public FieldOptionsAttribute(Type optionsType)
    {
        OptionsType = optionsType;
    }

    /// <summary>
    /// OptionsType
    /// </summary>
    public Type OptionsType { get; }
}
