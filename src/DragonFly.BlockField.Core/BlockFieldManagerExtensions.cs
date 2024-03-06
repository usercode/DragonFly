// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.BlockField;

namespace DragonFly;

public static class BlockFieldManagerExtensions
{
    /// <summary>
    /// Gets the block field manager.
    /// </summary>
    public static BlockFieldManager BlockField(this IDragonFlyApi api)
    {
        return BlockFieldManager.Default;
    }

    public static void AddDefaults(this BlockFieldManager manager)
    {
        manager.Add<ColumnBlock>();
        manager.Add<GridBlock>();
        manager.Add<AssetBlock>();
        manager.Add<SlideshowBlock>();
        manager.Add<TextBlock>();
        manager.Add<HtmlBlock>();
        manager.Add<YouTubeBlock>();
        manager.Add<CodeBlock>();
        manager.Add<OpenGraphBlock>();
        manager.Add<HeadingBlock>();
        manager.Add<QuoteBlock>();
        manager.Add<ReferenceBlock>();
        manager.Add<ContainerBlock>();
        manager.Add<AlertBlock>();
        manager.Add<ProgressBlock>();
        manager.Add<CardsBlock>();
        manager.Add<SectionBlock>();

        manager.Add<UnknownBlock>();
    }
}
