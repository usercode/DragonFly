// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DragonFly.API;

public static class JsonSerializerDefault
{
    private static JsonSerializerOptions? _options;

    /// <summary>
    /// Options
    /// </summary>
    public static JsonSerializerOptions Options
    {
        get
        {
            if (_options == null)
            {
                _options = new JsonSerializerOptions();

                //_options.TypeInfoResolverChain.Add(ApiJsonSerializerContext.Default);

                //build missing json field serializer
                foreach (Type contentFieldType in FieldManager.Default.GetAllFieldTypes())
                {
                    if (JsonFieldManager.Default.TryGetByFieldType(contentFieldType, out IJsonFieldSerializer? fieldSerializer))
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

                        JsonFieldManager.Default.Add(fieldSerializer);
                    }
                    else //build DefaultFieldSerializer
                    {
                        fieldSerializer = (IJsonFieldSerializer?)Activator.CreateInstance(typeof(DefaultJsonFieldSerializer<>).MakeGenericType(contentFieldType));

                        if (fieldSerializer == null)
                        {
                            throw new Exception($"Could not create default field serializer for '{contentFieldType.Name}'.");
                        }

                        JsonFieldManager.Default.Add(fieldSerializer);
                    }
                }

               // build derived types
                JsonPolymorphismOptions optionsDerivedTypes = new JsonPolymorphismOptions();

                foreach (Type type in FieldManager.Default.GetAllOptionsTypes())
                {
                    optionsDerivedTypes.DerivedTypes.Add(new JsonDerivedType(type, type.Name));
                }

                JsonPolymorphismOptions queryDerivedTypes = new JsonPolymorphismOptions();

                foreach (Type type in FieldManager.Default.GetAllQueryTypes())
                {
                    queryDerivedTypes.DerivedTypes.Add(new JsonDerivedType(type, type.Name));
                }

                _options.TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                {
                    Modifiers =
                    {
                        (JsonTypeInfo typeInfo) =>
                        {
                            if (typeInfo.Type == typeof(FieldOptions))
                            {
                                typeInfo.PolymorphismOptions = optionsDerivedTypes;
                            }
                            else if (typeInfo.Type == typeof(FieldQuery))
                            {
                                typeInfo.PolymorphismOptions = queryDerivedTypes;
                            }
                        }
                    }
                };
            }

            return _options;
        }
    }
}
