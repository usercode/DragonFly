using DragonFly.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Pages.ContentItems.Query;

public interface IFieldQueryComponent
{
    SchemaField Field { get; }

    FieldQuery Query { get; }
}
