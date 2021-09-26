using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Blocks
{
    /// <summary>
    /// ImageElement
    /// </summary>
    public class ImageElement : Element
    {
        public override string Name => "Image";

        public Guid? AssetId { get; set; }
    }
}
