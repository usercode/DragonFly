// Copyright (c) usercode
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
        if (Data == null)
        {
            return;
        }

        IEnumerable<Block> innerBlock = await BlockFieldSerializer.DeserializeBlockAsync(Data);

        int i = 0;

        foreach(Block block in innerBlock) 
        {
            blocks.Insert(index + i, block);

            i++;
        }
    }
}
