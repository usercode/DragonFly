using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// IntegerQuery
    /// </summary>
    public class IntegerQuery : FieldQuery
    {
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
            return MinValue == null && MaxValue == null;
        }
    }
}
