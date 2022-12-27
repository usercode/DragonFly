// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.Builders;
using DragonFly.Storage.Abstractions;
using DragonFly.Storage.MongoDB.Fields;

namespace DragonFly.MongoDB.Serializers;

/// <summary>
/// CreateMissingMongoFieldSerializer
/// </summary>
public class CreateMissingMongoFieldSerializer : IPostInitialize
{
    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        foreach (Type contentFieldType in api.ContentField().GetAllFieldTypes())
        {
            if (api.MongoField().TryGetByFieldType(contentFieldType, out IMongoFieldSerializer? fieldSerializer))
            {
                continue;
            }

            //build SingleValueSerializer?
            if (contentFieldType.GetInterfaces().Any(x => x == typeof(ISingleValueField)))
            {
                //create SingleValueFieldSerializer
                fieldSerializer = (IMongoFieldSerializer?)Activator.CreateInstance(typeof(SingleValueMongoFieldSerializer<>).MakeGenericType(contentFieldType));

                if (fieldSerializer == null)
                {
                    throw new Exception($"Could not create single value field serializer for '{contentFieldType.Name}'.");
                }

                api.MongoField().RegisterField(fieldSerializer);
            }
            else //build DefaultFieldSerializer
            {
                fieldSerializer = (IMongoFieldSerializer?)Activator.CreateInstance(typeof(DefaultMongoFieldSerializer<>).MakeGenericType(contentFieldType));

                if (fieldSerializer == null)
                {
                    throw new Exception($"Could not create default field serializer for '{contentFieldType.Name}'.");
                }

                api.MongoField().RegisterField(fieldSerializer);
            }
        }
    }
}
