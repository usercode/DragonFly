// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class FieldComponent<TField, TFieldOptions> : ComponentBase, IFieldComponent
    where TField : ContentField
    where TFieldOptions : ContentFieldOptions
{
    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public TFieldOptions Options { get; set; }

    ContentField IFieldComponent.Field => Field;

    ContentFieldOptions IFieldComponent.Options => Options;
}
