using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Query;

public abstract class FieldQueryComponent<TQuery> : ComponentBase, IFieldQueryComponent
    where TQuery : FieldQuery
{
    public SchemaField Field { get; set; }

    [Parameter]
    public TQuery Query { get; set; }

    FieldQuery IFieldQueryComponent.Query => Query;
}
