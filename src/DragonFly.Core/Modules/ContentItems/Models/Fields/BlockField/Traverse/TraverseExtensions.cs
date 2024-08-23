// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class TraverseExtensions
{
    public static IEnumerable<BlockContext> EnumerateBlocks(this IChildBlocks input)
    {
        foreach (BlockContext context in input.GetBlocks())
        {
            yield return context;

            if (context.Block is IChildBlocks childBlock)
            {
                foreach (BlockContext b in EnumerateBlocks(childBlock))
                {
                    yield return b;
                }
            }
        }
    }
}
