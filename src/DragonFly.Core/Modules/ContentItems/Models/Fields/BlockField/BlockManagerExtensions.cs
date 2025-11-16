// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class BlockManagerExtensions
{
    extension(IDragonFlyApi api)
    {
        public BlockManager Blocks => BlockManager.Default;
    }

    public static BlockManager AddDefaults(this BlockManager manager)
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

        return manager;
    }
}
