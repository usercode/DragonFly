using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Razor.Base
{
    /// <summary>
    /// BlockComponent
    /// </summary>
    /// <typeparam name="TBlock"></typeparam>
    public class BlockComponent<TBlock> : ComponentBase, IBlockComponent
        where TBlock : Block
    {
        [Parameter]
        public TBlock Block { get; set; }

        Block IBlockComponent.Block => Block;

    }
}
