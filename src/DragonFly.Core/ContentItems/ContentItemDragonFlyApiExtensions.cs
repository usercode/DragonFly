// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentItemDragonFlyApiExtensions
/// </summary>
public static class ContentItemDragonFlyApiExtensions
{
    public static ContentFieldManager ContentField(this IDragonFlyApi dragonFlyApi)
    {
        return ContentFieldManager.Default;
    }

    public static ContentFieldManager AddDefaults(this ContentFieldManager manager)
    {
        manager.Add<ArrayField>();
        manager.Add<AssetField>();
        manager.Add<BoolField>();
        manager.Add<DateTimeField>();
        manager.Add<EmbedField>();
        manager.Add<FloatField>();
        manager.Add<HtmlField>();
        manager.Add<IntegerField>();
        manager.Add<ReferenceField>();
        manager.Add<SlugField>();
        manager.Add<StringField>();
        manager.Add<TextField>();
        manager.Add<XHtmlField>();
        manager.Add<XmlField>();

        return manager;
    }
}
