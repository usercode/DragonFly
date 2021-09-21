using DragonFly.Core.ContentItems.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// StringQuery
    /// </summary>
    public class StringQuery : FieldQueryBase
    {
        /// <summary>
        /// Pattern
        /// </summary>
        public string? Pattern { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public StringQueryType Type { get; set; }
    }
}
