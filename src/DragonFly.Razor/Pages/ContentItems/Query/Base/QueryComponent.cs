using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Query
{
    public abstract class QueryComponent<TQuery> : ComponentBase, IQueryComponent
        where TQuery : FieldQuery
    {
        public SchemaField Field { get; set; }

        [Parameter]
        public TQuery Query { get; set; }

        FieldQuery IQueryComponent.Query => Query;
    }
}
