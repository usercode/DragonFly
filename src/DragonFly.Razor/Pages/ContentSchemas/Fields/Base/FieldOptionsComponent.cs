using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentSchemas.Fields
{
    public abstract class FieldOptionsComponent<TFieldOptions> : ComponentBase, IFieldOptionsComponent
        where TFieldOptions : ContentFieldOptions
    {
        [Parameter]
        public TFieldOptions Options { get; set; }

        ContentFieldOptions IFieldOptionsComponent.Options { get => Options; }
    }
}
