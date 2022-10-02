// Copyright (c) usercode
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

/// <summary>
/// FieldComponent
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public abstract class FieldComponent<TField> : ComponentBase, IFieldComponent
    where TField : IContentField
{
    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public ContentFieldOptions Options { get; set; }

    IContentField IFieldComponent.Field => Field;
}
