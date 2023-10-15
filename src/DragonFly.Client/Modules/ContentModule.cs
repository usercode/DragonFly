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

        api.RegisterField<ArrayField, ArrayFieldView, ArrayFieldOptionsView>();
        api.RegisterField<AssetField, AssetFieldView, AssetFieldOptionsView, AssetFieldQueryView>();
        api.RegisterField<ReferenceField, ReferenceFieldView, ReferenceFieldOptionsView, ReferenceFieldQueryView>();
        api.RegisterField<BoolField, BoolFieldView, BoolFieldOptionsView, BoolFieldQueryView>();
        api.RegisterField<ComponentField, ComponentFieldView, ComponentFieldOptionsView>();
        api.RegisterField<DateTimeField, DateTimeFieldView>();
        api.RegisterField<StringField, StringFieldView, StringFieldOptionsView, StringFieldQueryView>();
        api.RegisterField<FloatField, FloatFieldView, FloatFieldOptionsView, FloatFieldQueryView>();
        api.RegisterField<IntegerField, IntegerFieldView, IntegerFieldOptionsView, IntegerFieldQueryView>();
        api.RegisterField<SlugField, SlugFieldView>();
        api.RegisterField<TextField, TextFieldView>();
        api.RegisterField<HtmlField, HtmlFieldView>();
        api.RegisterField<XHtmlField, XHtmlFieldView>();
        api.RegisterField<XmlField, XmlFieldView>();
        api.RegisterField<ColorField, ColorFieldView>();
        api.RegisterField<GeolocationField, GeolocationFieldView, GeolocationFieldOptionsView, GeolocationFieldQueryView>();
    }
}
