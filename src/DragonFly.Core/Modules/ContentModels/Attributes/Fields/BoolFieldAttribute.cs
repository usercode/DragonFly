// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

public class BoolFieldAttribute : BaseFieldAttribute
{
    public bool Index { get; set; }

    public override void AddToSchema(ContentSchema schema, string property)
    {
        schema.AddBool(property, x => 
                                    { 
                                        x.DefaultValue = false; 
                                        x.IsRequired = Required; 
                                        x.IsSearchable = Index; 
                                    }, 
                                    SortKey);

        if (ListField)
        {
            schema.ListFields.Add(property);
        }
    }
}
