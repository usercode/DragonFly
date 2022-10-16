// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using GraphQL.Types;

namespace DragonFly.AspNetCore.GraphQL;

public class ContentSchemaType : ObjectGraphType<ContentSchema>
{
    public ContentSchemaType()
    {
        Name = "ContentSchema";

        Field(x => x.CreatedAt);
        Field(x => x.ModifiedAt);
        Field(x => x.Name);
        
    }
}
