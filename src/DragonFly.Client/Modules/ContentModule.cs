// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.ContentItems.Fields;
using DragonFly.Client.Pages.ContentItems.Query;
using DragonFly.Client.Pages.ContentSchemas.Fields;

namespace DragonFly.Client;

/// <summary>
/// ContentModule
/// </summary>
public class ContentModule : ClientModule
{
    public override string Name => "Content";

    public override string Description => "Manage content schema and items";

    public override string Author => "DragonFly";

    public override void Init(IDragonFlyApi api)
    {
        api.MainMenu().Add("Schema", "fa-solid fa-layer-group", "schema");
        //api.MainMenu().Add("Structure", "fa-solid fa-folder-tree", "structure");
        api.MainMenu().Add("Content", "fa-solid fa-list", "content");

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
        api.ContentField().Add<XHtmlField>().WithFieldView<XHtmlFieldView>();
        api.ContentField().Add<XmlField>().WithFieldView<XmlFieldView>();
        api.ContentField().Add<ColorField>().WithFieldView<ColorFieldView>();
        api.ContentField().Add<GeolocationField>().WithFieldView<GeolocationFieldView>().WithOptionView<GeolocationFieldOptionsView>().WithQueryView<GeolocationFieldQueryView>();
    }
}
