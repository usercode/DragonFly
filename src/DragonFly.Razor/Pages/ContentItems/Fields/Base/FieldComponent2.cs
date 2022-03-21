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
    where TField : ContentField
{
    [Parameter]
    public TField Field { get; set; }

    [Parameter]
    public ContentFieldOptions Options { get; set; }

    ContentField IFieldComponent.Field => Field;
}
