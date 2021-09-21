using DragonFly.Content;
using DragonFly.Core.ContentItems.Queries;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Query
{
    public abstract class QueryComponent<TQuery> : ComponentBase, IQueryComponent
        where TQuery : FieldQueryBase
    {
        public SchemaField Field { get; set; }

        [Parameter]
        public TQuery Query { get; set; }

        FieldQueryBase IQueryComponent.Query => Query;
    }
}
