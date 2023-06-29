// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class FieldComponent<TField, TFieldOptions> : ComponentBase, IFieldComponent
    where TField : ContentField
    where TFieldOptions : FieldOptions
{
    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public TFieldOptions Options { get; set; }

    ContentField IFieldComponent.Field => Field;

    FieldOptions IFieldComponent.Options => Options;
}
