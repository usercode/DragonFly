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
        api.MainMenu.Add("Schema", "fa-solid fa-layer-group", "schema");
        //api.MainMenu().Add("Structure", "fa-solid fa-folder-tree", "structure");
        api.MainMenu.Add("Content", "fa-solid fa-list", "content");

        //fields
        api.Fields.Add<ArrayField>().WithFieldView<ArrayFieldView>().WithOptionView<ArrayFieldOptionsView>();
        api.Fields.Add<AssetField>().WithFieldView<AssetFieldView>().WithOptionView<AssetFieldOptionsView>().WithQueryView<AssetFieldQueryView>();
        api.Fields.Add<ReferenceField>().WithFieldView<ReferenceFieldView>().WithOptionView<ReferenceFieldOptionsView>().WithQueryView<ReferenceFieldQueryView>();
        api.Fields.Add<BoolField>().WithFieldView<BoolFieldView>().WithOptionView<BoolFieldOptionsView>().WithQueryView<BoolFieldQueryView>();
        api.Fields.Add<ComponentField>().WithFieldView<ComponentFieldView>().WithOptionView<ComponentFieldOptionsView>();
        api.Fields.Add<DateField>().WithFieldView<DateFieldView>();
        api.Fields.Add<TimeField>().WithFieldView<TimeFieldView>();
        api.Fields.Add<DateTimeField>().WithFieldView<DateTimeFieldView>();
        api.Fields.Add<StringField>().WithFieldView<StringFieldView>().WithOptionView<StringFieldOptionsView>().WithQueryView<StringFieldQueryView>();
        api.Fields.Add<FloatField>().WithFieldView<FloatFieldView>().WithOptionView<FloatFieldOptionsView>().WithQueryView<FloatFieldQueryView>();
        api.Fields.Add<IntegerField>().WithFieldView<IntegerFieldView>().WithOptionView<IntegerFieldOptionsView>().WithQueryView<IntegerFieldQueryView>();
        api.Fields.Add<SlugField>().WithFieldView<SlugFieldView>().WithOptionView<SlugFieldOptionsView>();
        api.Fields.Add<TextField>().WithFieldView<TextFieldView>();
        api.Fields.Add<HtmlField>().WithFieldView<HtmlFieldView>();
        api.Fields.Add<XmlField>().WithFieldView<XmlFieldView>();
        api.Fields.Add<ColorField>().WithFieldView<ColorFieldView>();
        api.Fields.Add<GeolocationField>().WithFieldView<GeolocationFieldView>().WithOptionView<GeolocationFieldOptionsView>().WithQueryView<GeolocationFieldQueryView>();
        api.Fields.Add<UrlField>().WithFieldView<UrlFieldView>().WithOptionView<UrlFieldOptionsView>();

        //blockfield
        api.Fields.Add<BlockField>().WithFieldView<BlockFieldView>();

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
