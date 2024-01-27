// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldFactory
/// </summary>
public sealed class FieldFactory(
                                string fieldName,
                                Type fieldType,
                                Type? optionsType,
                                Type? queryType,
                                Func<ContentField> factoryField,
                                Func<FieldOptions>? factoryOptions,
                                Func<FieldQuery>? factoryQuery)
{
    /// <summary>
    /// FieldName
    /// </summary>
    public string FieldName { get; } = fieldName;

    /// <summary>
    /// FieldType
    /// </summary>
    public Type FieldType { get; } = fieldType;

    /// <summary>
    /// OptionsType
    /// </summary>
    public Type? OptionsType { get; } = optionsType;

    /// <summary>
    /// QueryType
    /// </summary>
    public Type? QueryType { get; } = queryType;

    /// <summary>
    /// CreateField
    /// </summary>
    public ContentField CreateField() => factoryField();

    /// <summary>
    /// CreateOptions
    /// </summary>
    public FieldOptions? CreateOptions() => factoryOptions?.Invoke();

    /// <summary>
    /// CreateQuery
    /// </summary>
    public FieldQuery? CreateQuery() => factoryQuery?.Invoke();
}
