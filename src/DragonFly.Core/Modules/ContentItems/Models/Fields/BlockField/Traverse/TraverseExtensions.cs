// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

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
