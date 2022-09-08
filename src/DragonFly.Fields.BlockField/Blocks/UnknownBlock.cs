using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// UnknownBlock
/// </summary>
public class UnknownBlock : Block
{
    public UnknownBlock()
    {

    }

    public UnknownBlock(string? content)
    {
        Content = content;
    }

    public string? Content { get; set; }
}
