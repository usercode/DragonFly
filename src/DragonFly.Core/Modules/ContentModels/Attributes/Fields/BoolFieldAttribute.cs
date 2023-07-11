// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class BoolFieldAttribute : BaseFieldAttribute
{
    public override void AddToSchema(ContentSchema schema, string property)
    {
        base.AddToSchema(schema, property);

        schema.AddBool(property, x => 
                                    { 
                                        x.DefaultValue = false; 
                                        x.IsRequired = Required; 
                                        x.IsSearchable = Index; 
                                    }, 
                                    SortKey);
    }
}
