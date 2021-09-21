using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Queries
{
    public abstract class FieldQueryBase
    {
        public virtual string? FieldName { get; set; }
    }
}
