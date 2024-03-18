// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Client.Pages.ContentItems.Fields;
using DragonFly.Client.Pages.ContentItems.Fields.Blocks;
using DragonFly.Client.Pages.ContentItems.Query;
using DragonFly.Client.Pages.ContentSchemas.Fields;
using DragonFly.Init;

namespace DragonFly.Client;

/// <summary>
/// ContentModule
/// </summary>
public class ContentInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {       
        api.MainMenu().Add("Schema", "fa-solid fa-layer-group", "schema");
        //api.MainMenu().Add("Structure", "fa-solid fa-folder-tree", "structure");
        api.MainMenu().Add("Content", "fa-solid fa-list", "content");

        //fields
        api.ContentField().Add<ArrayField>().WithFieldView<ArrayFieldView>().WithOptionView<ArrayFieldOptionsView>();
        api.ContentField().Add<AssetField>().WithFieldView<AssetFieldView>().WithOptionView<AssetFieldOptionsView>().WithQueryView<AssetFieldQueryView>();
        api.ContentField().Add<ReferenceField>().WithFieldView<ReferenceFieldView>().WithOptionView<ReferenceFieldOptionsView>().WithQueryView<ReferenceFieldQueryView>();
        api.ContentField().Add<BoolField>().WithFieldView<BoolFieldView>().WithOptionView<BoolFieldOptionsView>().WithQueryView<BoolFieldQueryView>();
        api.ContentField().Add<ComponentField>().WithFieldView<ComponentFieldView>().WithOptionView<ComponentFieldOptionsView>();
        api.ContentField().Add<DateTimeField>().WithFieldView<DateTimeFieldView>();
        api.ContentField().Add<StringField>().WithFieldView<StringFieldView>().WithOptionView<StringFieldOptionsView>().WithQueryView<StringFieldQueryView>();
        api.ContentField().Add<FloatField>().WithFieldView<FloatFieldView>().WithOptionView<FloatFieldOptionsView>().WithQueryView<FloatFieldQueryView>();
        api.ContentField().Add<IntegerField>().WithFieldView<IntegerFieldView>().WithOptionView<IntegerFieldOptionsView>().WithQueryView<IntegerFieldQueryView>();
        api.ContentField().Add<SlugField>().WithFieldView<SlugFieldView>();
        api.ContentField().Add<TextField>().WithFieldView<TextFieldView>();
        api.ContentField().Add<HtmlField>().WithFieldView<HtmlFieldView>();
        api.ContentField().Add<XmlField>().WithFieldView<XmlFieldView>();
        api.ContentField().Add<ColorField>().WithFieldView<ColorFieldView>();
        api.ContentField().Add<GeolocationField>().WithFieldView<GeolocationFieldView>().WithOptionView<GeolocationFieldOptionsView>().WithQueryView<GeolocationFieldQueryView>();
        api.ContentField().Add<UrlField>().WithFieldView<UrlFieldView>().WithOptionView<UrlFieldOptionsView>();

        //blockfield
        api.ContentField().Add<BlockField>().WithFieldView<BlockFieldView>();

        //blocks
        api.RegisterBlock<ColumnBlock, ColumnBlockView>();
        api.RegisterBlock<GridBlock, GridBlockView>();
        api.RegisterBlock<AssetBlock, AssetBlockView>();
        api.RegisterBlock<SlideshowBlock, SlideshowBlockView>();
        api.RegisterBlock<TextBlock, TextBlockView>();
        api.RegisterBlock<HtmlBlock, HtmlBlockView>();
        api.RegisterBlock<YouTubeBlock, YouTubeBlockView>();
        api.RegisterBlock<CodeBlock, CodeBlockView>();
        api.RegisterBlock<OpenGraphBlock, OpenGraphView>();
        api.RegisterBlock<HeadingBlock, HeadingBlockView>();
        api.RegisterBlock<QuoteBlock, QuoteBlockView>();
        api.RegisterBlock<ReferenceBlock, ReferenceBlockView>();
        api.RegisterBlock<ContainerBlock, ContainerBlockView>();
        api.RegisterBlock<AlertBlock, AlertBlockView>();
        api.RegisterBlock<ProgressBlock, ProgressBlockView>();
        api.RegisterBlock<CardsBlock, CardsBlockView>();
        api.RegisterBlock<SectionBlock, SectionBlockView>();

        return Task.CompletedTask;
    }
}
