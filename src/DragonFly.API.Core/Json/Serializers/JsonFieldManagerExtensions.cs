// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;

namespace DragonFly;

public static class JsonFieldManagerExtensions
{
    public static JsonFieldManager JsonFields(this IDragonFlyApi api)
    {
        return JsonFieldManager.Default;
    }

    public static JsonFieldManager AddDefaults(this JsonFieldManager json)
    {
        json.Add<ArrayJsonFieldSerializer>();
        json.Add<AssetJsonFieldSerializer>();
        json.Add<ComponentJsonFieldSerializer>();
        json.Add<ReferenceJsonFieldSerializer>();

        json.Add<SingleValueJsonFieldSerializer<BoolField>>();
        json.Add<SingleValueJsonFieldSerializer<StringField>>();
        json.Add<SingleValueJsonFieldSerializer<SlugField>>();
        json.Add<SingleValueJsonFieldSerializer<ColorField>>();
        json.Add<SingleValueJsonFieldSerializer<IntegerField>>();
        json.Add<SingleValueJsonFieldSerializer<FloatField>>();
        json.Add<SingleValueJsonFieldSerializer<TextField>>();
        json.Add<SingleValueJsonFieldSerializer<HtmlField>>();
        json.Add<SingleValueJsonFieldSerializer<XHtmlField>>();
        json.Add<SingleValueJsonFieldSerializer<XmlField>>();
        json.Add<SingleValueJsonFieldSerializer<DateTimeField>>();

        json.Add<DefaultJsonFieldSerializer<GeolocationField>>();

        return json;
    }
}
