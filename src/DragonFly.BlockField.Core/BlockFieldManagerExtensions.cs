// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

public static class BlockFieldManagerExtensions
{
    public static BlockFieldManager BlockFields(this IDragonFlyApi api)
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
        manager.Add<CardBlock>();

        manager.Add<UnknownBlock>();
    }
}
