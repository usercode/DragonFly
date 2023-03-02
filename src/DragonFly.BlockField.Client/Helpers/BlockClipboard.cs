﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField.Client.Helpers;

public static class BlockClipboard
{
    public static byte[] Data { get; set; }

    public static bool HasData() => Data != null;

    public static async Task CopyAsync(IEnumerable<Block> block)
    {
        Data = await BlockFieldSerializer.SerializeBlockAsync(block);
    }

    public static async Task PasteAsync(int index, IList<Block> blocks)
    {
        Block[] innerBlock = await BlockFieldSerializer.DeserializeBlockAsync(Data);

        for (int i = 0; i < innerBlock.Length; i++)
        {
            blocks.Insert(index + i, innerBlock[i]);
        }
    }
}
