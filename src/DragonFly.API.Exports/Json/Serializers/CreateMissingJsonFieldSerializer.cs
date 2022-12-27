// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API.Json;

/// <summary>
/// CreateMissingJsonFieldSerializer
/// </summary>
public class CreateMissingJsonFieldSerializer : IPostInitialize
{
    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        foreach (Type contentFieldType in api.ContentField().GetAllFieldTypes())
        {
            if (api.JsonField().TryGetByFieldType(contentFieldType, out IJsonFieldSerializer? fieldSerializer))
            {
                continue;
            }

            //build SingleValueSerializer?
            if (contentFieldType.GetInterfaces().Any(x => x == typeof(ISingleValueField)))
            {
                //create SingleValueFieldSerializer
                fieldSerializer = (IJsonFieldSerializer?)Activator.CreateInstance(typeof(SingleValueJsonFieldSerializer<>).MakeGenericType(contentFieldType));

                if (fieldSerializer == null)
                {
                    throw new Exception($"Could not create single value field serializer for '{contentFieldType.Name}'.");
                }

                api.JsonField().RegisterField(fieldSerializer);
            }
            else //build DefaultFieldSerializer
            {
                fieldSerializer = (IJsonFieldSerializer?)Activator.CreateInstance(typeof(DefaultJsonFieldSerializer<>).MakeGenericType(contentFieldType));

                if (fieldSerializer == null)
                {
                    throw new Exception($"Could not create default field serializer for '{contentFieldType.Name}'.");
                }

                api.JsonField().RegisterField(fieldSerializer);
            }
        }
    }
}
