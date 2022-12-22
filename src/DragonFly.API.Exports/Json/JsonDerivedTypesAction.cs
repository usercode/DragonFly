// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization.Metadata;
using DragonFly.AspNetCore.API.Exports.Json;
using DragonFly.Query;

namespace DragonFly.API.Exports.Json;

public class JsonDerivedTypesAction : IPostInitialize
{
    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        JsonPolymorphismOptions optionsDerivedTypes = new JsonPolymorphismOptions();

        foreach (Type type in api.ContentField().GetAllOptionsTypes())
        {
            optionsDerivedTypes.DerivedTypes.Add(new JsonDerivedType(type, type.Name));
        }

        JsonPolymorphismOptions queryDerivedTypes = new JsonPolymorphismOptions();

        foreach (Type type in api.ContentField().GetAllQueryTypes())
        {
            queryDerivedTypes.DerivedTypes.Add(new JsonDerivedType(type, type.Name));
        }

        JsonSerializerDefault.Options.TypeInfoResolver = new DefaultJsonTypeInfoResolver()
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
}
