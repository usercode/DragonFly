// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldAdded
/// </summary>
public sealed class FieldAdded
{
    public FieldAdded(Type fieldType, Type? optionsType, Type? queryType)
    {
        FieldType = fieldType;
        OptionsType = optionsType;
        QueryType = queryType;
    }

    /// <summary>
    /// FieldType
    /// </summary>
    public Type FieldType { get; }

    /// <summary>
    /// OptionsType
    /// </summary>
    public Type? OptionsType { get; }

    /// <summary>
    /// QueryType
    /// </summary>
    public Type? QueryType { get; }
}
