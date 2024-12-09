// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class BoolFieldAttribute : BaseFieldAttribute
{
    public override void ApplyToSchema(ContentSchema schema, string property)
    {
        base.ApplyToSchema(schema, property);

        schema.AddBool(property, x => 
                                    { 
                                        x.DefaultValue = false; 
                                        x.IsRequired = Required; 
                                        x.HasIndex = Index;
                                    }, 
                                    SortKey);
    }
}
