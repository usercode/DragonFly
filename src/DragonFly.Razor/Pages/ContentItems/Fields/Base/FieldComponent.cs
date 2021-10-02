using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Fields
{
    public abstract class FieldComponent<TContentField, TContentFieldOptions> : ComponentBase, IFieldComponent
        where TContentField : ContentField
        where TContentFieldOptions : ContentFieldOptions
    {
        [Parameter]
        public TContentField Field { get; set; }

        [Parameter]
        public TContentFieldOptions Options { get; set; }

        ContentField IFieldComponent.Field => Field;

        ContentFieldOptions IFieldComponent.Options => Options;
    }
}
