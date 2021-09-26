using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// ColumnElement
    /// </summary>
    public class ColumnElement : Element
    {
        /// <summary>
        /// Width
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Element
        /// </summary>
        public Element? Element { get; set; }
    }
}
