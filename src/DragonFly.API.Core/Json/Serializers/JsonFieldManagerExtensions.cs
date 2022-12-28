// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;

namespace DragonFly;

public static class JsonFieldManagerExtensions
{
    public static JsonFieldManager JsonField(this IDragonFlyApi api)
    {
        return JsonFieldManager.Default;
    }

    public static JsonFieldManager AddDefaults(this JsonFieldManager json)
    {
        json.RegisterField<ArrayJsonFieldSerializer>();
        json.RegisterField<AssetJsonFieldSerializer>();
        json.RegisterField<ComponentJsonFieldSerializer>();
        json.RegisterField<ReferenceJsonFieldSerializer>();

        json.RegisterField<SingleValueJsonFieldSerializer<BoolField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<StringField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<SlugField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<ColorField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<IntegerField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<FloatField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<TextField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<HtmlField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<XHtmlField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<XmlField>>();
        json.RegisterField<SingleValueJsonFieldSerializer<DateTimeField>>();

        json.RegisterField<DefaultJsonFieldSerializer<GeolocationField>>();

        return json;
    }
}
