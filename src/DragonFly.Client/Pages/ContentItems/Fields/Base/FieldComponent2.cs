// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// FieldComponent
/// </summary>
public abstract class FieldComponent<TField> : ComponentBase, IFieldComponent<TField>
    where TField : ContentField
{
    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public FieldOptions Options { get; set; }

    ContentField IFieldComponent.Field => Field;

    Type IFieldComponent.FieldType => typeof(TField);
}
