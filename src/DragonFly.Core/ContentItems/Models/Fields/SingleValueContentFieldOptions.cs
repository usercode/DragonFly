using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Data.Content.ContentParts
{
    public abstract class SingleValueContentFieldOptions<T> : ContentFieldOptions
        where T : struct
    {
        public bool IsRequired { get; set; }

        public T? DefaultValue { get; set; }
    }
}
