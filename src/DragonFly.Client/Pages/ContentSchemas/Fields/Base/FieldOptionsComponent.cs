// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class FieldOptionsComponent<TFieldOptions> : ComponentBase, IFieldOptionsComponent
    where TFieldOptions : FieldOptions
{
    [Parameter]
    public TFieldOptions Options { get; set; }

    FieldOptions IFieldOptionsComponent.Options { get => Options; }

    Type IFieldOptionsComponent.OptionsType => typeof(TFieldOptions);
}
