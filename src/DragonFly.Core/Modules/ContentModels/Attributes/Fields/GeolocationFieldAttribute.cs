// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class GeolocationFieldAttribute : BaseFieldAttribute
{
    public GeolocationFieldAttribute()
    {
    }

    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddGeolocation(property, x =>
                                        {
                                            x.IsRequired = Required;
                                            x.HasIndex = Index;
                                        },
                                        SortKey);
    }
}
