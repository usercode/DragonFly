using System.Collections.Generic;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// Block
    /// </summary>
    public class BlockElement : Element
    {
        public BlockElement()
        {
            Columns = new List<ColumnElement>();
        }

        /// <summary>
        /// Columns
        /// </summary>
        public IList<ColumnElement> Columns { get; set; }
    }
}