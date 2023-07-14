// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldFactory
/// </summary>
public sealed class FieldFactory
{
    public FieldFactory(
                string fieldName, 
                Type fieldType, 
                Type? optionsType, 
                Type? queryType, 
                Func<ContentField> factoryField, 
                Func<FieldOptions>? factoryOptions,
                Func<FieldQuery>? factoryQuery)
    {
        FieldName = fieldName;
        FieldType = fieldType;
        OptionsType = optionsType;
        QueryType = queryType;
        _createField = factoryField;
        _createOptions = factoryOptions;
        _createQuery = factoryQuery;
    }

    /// <summary>
    /// FieldName
    /// </summary>
    public string FieldName { get; }

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


    private Func<ContentField> _createField;

    /// <summary>
    /// CreateField
    /// </summary>
    public ContentField CreateField() => _createField();

    private Func<FieldOptions>? _createOptions;

    /// <summary>
    /// CreateOptions
    /// </summary>
    public FieldOptions? CreateOptions() => _createOptions?.Invoke();

    private Func<FieldQuery>? _createQuery;

    /// <summary>
    /// CreateQuery
    /// </summary>
    public FieldQuery? CreateQuery() => _createQuery?.Invoke();
}
