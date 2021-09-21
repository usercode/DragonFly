using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Queries.Fields.Integers
{
    /// <summary>
    /// IntegerQuery
    /// </summary>
    public class IntegerQuery : FieldQueryBase
    {
        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public IntegerQueryOperator Operator { get; set; }
    }
}
