using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// Document
    /// </summary>
    public class Document
    {
        public Document()
        {
            Blocks = new List<Element>();
        }

        /// <summary>
        /// Blocks
        /// </summary>
        public IList<Element> Blocks { get; set; }
    }
}
