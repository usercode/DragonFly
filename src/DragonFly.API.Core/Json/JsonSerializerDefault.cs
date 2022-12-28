// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DragonFly.Query;

namespace DragonFly.API;

public class JsonSerializerDefault
{
    private static JsonSerializerOptions? _options;

    public static JsonSerializerOptions Options
    {
        get
        {
            if (_options == null)
            {
                _options = new JsonSerializerOptions();

                JsonPolymorphismOptions optionsDerivedTypes = new JsonPolymorphismOptions();

                foreach (Type type in ContentFieldManager.Default.GetAllOptionsTypes())
                {
                    optionsDerivedTypes.DerivedTypes.Add(new JsonDerivedType(type, type.Name));
                }

                JsonPolymorphismOptions queryDerivedTypes = new JsonPolymorphismOptions();

                foreach (Type type in ContentFieldManager.Default.GetAllQueryTypes())
                {
                    queryDerivedTypes.DerivedTypes.Add(new JsonDerivedType(type, type.Name));
                }

                _options.TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                {
                    Modifiers =
                    {
                        (JsonTypeInfo typeInfo) =>
                        {
                            if (typeInfo.Type == typeof(ContentFieldOptions))
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
