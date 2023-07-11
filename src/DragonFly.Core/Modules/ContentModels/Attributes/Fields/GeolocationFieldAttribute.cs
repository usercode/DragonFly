// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class GeolocationFieldAttribute : BaseFieldAttribute
{
    public GeolocationFieldAttribute()
    {
    }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        base.AddToSchema(schema, property);

        schema.AddGeolocation(property, x =>
                                        {
                                            x.IsRequired = Required;
                                            x.IsSearchable = Index;
                                        },
                                        SortKey);
    }
}
