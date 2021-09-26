using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Blocks
{
    /// <summary>
    /// HtmlElement
    /// </summary>
    public class PlainTextElement : Element
    {
        public override string Name => "PlainText";

        public string? Text { get; set; }
    }
}
