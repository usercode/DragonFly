using DragonFly.Content;
using DragonFly.Core.ContentItems.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Query
{
    public interface IQueryComponent
    {
        SchemaField Field { get; }

        FieldQueryBase Query { get; }
    }
}
