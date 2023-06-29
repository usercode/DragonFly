// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class FieldOptionsComponent<TFieldOptions> : ComponentBase, IFieldOptionsComponent
    where TFieldOptions : FieldOptions
{
    [Parameter]
    public TFieldOptions Options { get; set; }

    FieldOptions IFieldOptionsComponent.Options { get => Options; }
}
