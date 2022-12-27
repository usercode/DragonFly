// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB;
using DragonFly.Storage.MongoDB.Fields;

namespace DragonFly;

public static class MongoFieldManagerExtensions
{
    public static MongoFieldManager MongoField(this IDragonFlyApi api)
    {
        return MongoFieldManager.Default;
    }

    public static MongoFieldManager AddDefaults(this MongoFieldManager mongoFieldManager)
    {
        mongoFieldManager.RegisterField<ArrayMongoFieldSerializer>();
        mongoFieldManager.RegisterField<AssetMongoFieldSerializer>();
        mongoFieldManager.RegisterField<ComponentMongoFieldSerializer>();
        mongoFieldManager.RegisterField<ReferenceMongoFieldSerializer>();

        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<BoolField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<StringField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<SlugField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<ColorField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<IntegerField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<FloatField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<TextField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<HtmlField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<XHtmlField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<XmlField>>();
        mongoFieldManager.RegisterField<SingleValueMongoFieldSerializer<DateTimeField>>();

        mongoFieldManager.RegisterField<DefaultMongoFieldSerializer<GeolocationField>>();

        return mongoFieldManager;
    }
}
