// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class FieldOptionsComponent<TFieldOptions> : ComponentBase, IFieldOptionsComponent
    where TFieldOptions : ContentFieldOptions
{
    [Parameter]
    public TFieldOptions Options { get; set; }

    ContentFieldOptions IFieldOptionsComponent.Options { get => Options; }
}
