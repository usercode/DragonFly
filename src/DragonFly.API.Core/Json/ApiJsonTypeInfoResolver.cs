// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DragonFly.API;

public class ApiJsonTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    /// <summary>
    /// Default
    /// </summary>
    public static ApiJsonTypeInfoResolver Default { get; } = new ApiJsonTypeInfoResolver();

    private ApiJsonTypeInfoResolver()
    {
    }

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonInfoType = base.GetTypeInfo(type, options);

        if (jsonInfoType.Type == typeof(FieldOptions))
        {
            JsonPolymorphismOptions optionsDerivedTypes = new JsonPolymorphismOptions();

            foreach (Type t in FieldManager.Default.GetAllOptionsTypes())
            {
                optionsDerivedTypes.DerivedTypes.Add(new JsonDerivedType(t, t.Name));
            }

            jsonInfoType.PolymorphismOptions = optionsDerivedTypes;
        }
        else if (jsonInfoType.Type == typeof(FieldQuery))
        {
            JsonPolymorphismOptions queryDerivedTypes = new JsonPolymorphismOptions();

            foreach (Type t in FieldManager.Default.GetAllQueryTypes())
            {
                queryDerivedTypes.DerivedTypes.Add(new JsonDerivedType(t, t.Name));
            }

            jsonInfoType.PolymorphismOptions = queryDerivedTypes;
        }

        return jsonInfoType;
    }
}
