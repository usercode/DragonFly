using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// StringFieldQuery
    /// </summary>
    public class StringFieldQuery : FieldQuery
    {
        /// <summary>
        /// Pattern
        /// </summary>
        public string? Pattern { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public StringFieldQueryType Type { get; set; }

        public override bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Pattern);
        }
    }
}
