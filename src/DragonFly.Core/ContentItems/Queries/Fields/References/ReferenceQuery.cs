using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// ReferenceQuery
    /// </summary>
    public class ReferenceQuery : FieldQuery
    {
        /// <summary>
        /// ContentItemId
        /// </summary>
        public Guid? ContentItemId { get; set; }

        public override bool IsEmpty() => ContentItemId == null;
    }
}
