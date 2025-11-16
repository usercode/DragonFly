// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB;
using DragonFly.Storage.MongoDB.Fields;

namespace DragonFly;

public static class MongoFieldManagerExtensions
{
    extension(IDragonFlyApi api)
    {
        public MongoFieldManager MongoFields => MongoFieldManager.Default;
    }

    public static MongoFieldManager AddDefaults(this MongoFieldManager manager)
    {
        manager.Add<ArrayMongoFieldSerializer>();
        manager.Add<AssetMongoFieldSerializer>();
        manager.Add<ComponentMongoFieldSerializer>();
        manager.Add<ReferenceMongoFieldSerializer>();
        manager.Add<GeolocationMongoFieldSerializer>();

        manager.Add<SingleValueMongoFieldSerializer<BoolField>>();
        manager.Add<SingleValueMongoFieldSerializer<StringField>>();
        manager.Add<SingleValueMongoFieldSerializer<SlugField>>();
        manager.Add<SingleValueMongoFieldSerializer<ColorField>>();
        manager.Add<SingleValueMongoFieldSerializer<IntegerField>>();
        manager.Add<SingleValueMongoFieldSerializer<FloatField>>();
        manager.Add<SingleValueMongoFieldSerializer<TextField>>();
        manager.Add<SingleValueMongoFieldSerializer<HtmlField>>();
        manager.Add<SingleValueMongoFieldSerializer<XmlField>>();
        manager.Add<SingleValueMongoFieldSerializer<DateTimeField>>();        

        return manager;
    }
}
