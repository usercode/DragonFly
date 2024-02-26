// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// FieldComponent with options.
/// </summary>
public abstract class FieldComponent<TField, TFieldOptions> : ComponentBase, IFieldComponent<TField>
    where TField : ContentField
    where TFieldOptions : FieldOptions
{
    [Parameter]
    public string FieldName { get; set; }

    [Parameter]
    public bool IsReadOnly { get; set; }

    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public TFieldOptions Options { get; set; }

    ContentField IFieldComponent.Field => Field;

    Type IFieldComponent.FieldType => typeof(TField);

    FieldOptions IFieldComponent.Options => Options;
}

/// <summary>
/// FieldComponent without options.
/// </summary>
public abstract class FieldComponent<TField> : FieldComponent<TField, FieldOptions>
    where TField : ContentField
{
}
