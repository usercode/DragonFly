using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentField
    /// </summary>
    public abstract class SingleValueContentFieldNullable<T> : SingleValueContentField<T?>
        where T : struct
    {
        public SingleValueContentFieldNullable()
        {

        }

        public override bool HasValue => Value.HasValue;

    }
}
