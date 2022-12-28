// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// FieldComponent
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public abstract class FieldComponent<TField> : ComponentBase, IFieldComponent
    where TField : ContentField
{
    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public ContentFieldOptions Options { get; set; }

    ContentField IFieldComponent.Field => Field;
}
