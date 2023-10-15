// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldManagerExtensions
/// </summary>
public static class FieldManagerExtensions
{
    /// <summary>
    /// Gets the content field manager.
    /// </summary>
    /// <param name="api"></param>
    /// <returns></returns>
    public static FieldManager ContentField(this IDragonFlyApi api)
    {
        return FieldManager.Default;
    }

    public static FieldManager AddDefaults(this FieldManager manager)
    {
        manager.Add<ArrayField>();
        manager.Add<AssetField>();
        manager.Add<BoolField>();
        manager.Add<DateTimeField>();
        manager.Add<ComponentField>();
        manager.Add<FloatField>();
        manager.Add<HtmlField>();
        manager.Add<IntegerField>();
        manager.Add<ReferenceField>();
        manager.Add<SlugField>();
        manager.Add<StringField>();
        manager.Add<TextField>();
        manager.Add<XHtmlField>();
        manager.Add<XmlField>();
        manager.Add<ColorField>();
        manager.Add<GeolocationField>();

        return manager;
    }
}
