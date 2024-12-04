// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Client.Pages.ContentItems.Fields;
using DragonFly.Client.Pages.ContentItems.Fields.Blocks;
using DragonFly.Client.Pages.ContentItems.Query;
using DragonFly.Client.Pages.ContentSchemas.Fields;
using DragonFly.Init;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// ContentModule
/// </summary>
public class ContentInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {       
        api.MainMenu().Add("Schema", new Icons.Regular.Size24.AppsList(), "schema");
        //api.MainMenu().Add("Structure", "fa-solid fa-folder-tree", "structure");
        api.MainMenu().Add("Content", new Icons.Regular.Size24.AppFolder(), "content");

        //fields
        api.Field().Add<ArrayField>().WithFieldView<ArrayFieldView>().WithOptionView<ArrayFieldOptionsView>();
        api.Field().Add<AssetField>().WithFieldView<AssetFieldView>().WithOptionView<AssetFieldOptionsView>().WithQueryView<AssetFieldQueryView>();
        api.Field().Add<ReferenceField>().WithFieldView<ReferenceFieldView>().WithOptionView<ReferenceFieldOptionsView>().WithQueryView<ReferenceFieldQueryView>();
        api.Field().Add<BoolField>().WithFieldView<BoolFieldView>().WithOptionView<BoolFieldOptionsView>().WithQueryView<BoolFieldQueryView>();
        api.Field().Add<ComponentField>().WithFieldView<ComponentFieldView>().WithOptionView<ComponentFieldOptionsView>();
        api.Field().Add<DateField>().WithFieldView<DateFieldView>();
        api.Field().Add<TimeField>().WithFieldView<TimeFieldView>();
        api.Field().Add<DateTimeField>().WithFieldView<DateTimeFieldView>();
        api.Field().Add<StringField>().WithFieldView<StringFieldView>().WithOptionView<StringFieldOptionsView>().WithQueryView<StringFieldQueryView>();
        api.Field().Add<FloatField>().WithFieldView<FloatFieldView>().WithOptionView<FloatFieldOptionsView>().WithQueryView<FloatFieldQueryView>();
        api.Field().Add<IntegerField>().WithFieldView<IntegerFieldView>().WithOptionView<IntegerFieldOptionsView>().WithQueryView<IntegerFieldQueryView>();
        api.Field().Add<SlugField>().WithFieldView<SlugFieldView>().WithOptionView<SlugFieldOptionsView>();
        api.Field().Add<TextField>().WithFieldView<TextFieldView>();
        api.Field().Add<HtmlField>().WithFieldView<HtmlFieldView>();
        api.Field().Add<XmlField>().WithFieldView<XmlFieldView>();
        api.Field().Add<ColorField>().WithFieldView<ColorFieldView>();
        api.Field().Add<GeolocationField>().WithFieldView<GeolocationFieldView>().WithOptionView<GeolocationFieldOptionsView>().WithQueryView<GeolocationFieldQueryView>();
        api.Field().Add<UrlField>().WithFieldView<UrlFieldView>().WithOptionView<UrlFieldOptionsView>();

        //blockfield
        api.Field().Add<BlockField>().WithFieldView<BlockFieldView>();

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
