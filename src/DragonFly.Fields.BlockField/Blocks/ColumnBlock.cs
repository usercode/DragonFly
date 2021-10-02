using System.Collections.Generic;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// ColumnBlock
    /// </summary>
    public class ColumnBlock : Block
    {
        public ColumnBlock()
        {
            Columns = new List<Column>();
        }

        /// <summary>
        /// Columns
        /// </summary>
        public IList<Column> Columns { get; set; }
    }
}