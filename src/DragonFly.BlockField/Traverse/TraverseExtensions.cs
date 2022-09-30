using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

public static class TraverseExtensions
{
    public static IEnumerable<Block> EnumerateBlocks(this IChildBlocks input)
    {
        foreach (Block block in input.GetBlocks())
        {
            yield return block;

            if (block is IChildBlocks childBlock)
            {
                foreach (Block b in EnumerateBlocks(childBlock))
                {
                    yield return b;
                }
            }
        }
    }
}
