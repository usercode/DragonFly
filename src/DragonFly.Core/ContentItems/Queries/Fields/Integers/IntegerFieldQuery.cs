using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// IntegerFieldQuery
    /// </summary>
    public class IntegerFieldQuery : FieldQuery
    {
        /// <summary>
        /// MinValue
        /// </summary>
        public int? Value { get; set; }

        /// <summary>
        /// MinValue
        /// </summary>
        public int? MinValue { get; set; }

        /// <summary>
        /// MaxValue
        /// </summary>
        public int? MaxValue { get; set; }

        public override bool IsEmpty()
        {
            return Value == null && MinValue == null && MaxValue == null;
        }
    }
}
