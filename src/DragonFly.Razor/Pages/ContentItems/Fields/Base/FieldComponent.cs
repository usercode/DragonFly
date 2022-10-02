﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Fields;

public abstract class FieldComponent<TField, TFieldOptions> : ComponentBase, IFieldComponent
    where TField : IContentField
    where TFieldOptions : ContentFieldOptions
{
    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public TFieldOptions Options { get; set; }

    IContentField IFieldComponent.Field => Field;

    ContentFieldOptions IFieldComponent.Options => Options;
}
