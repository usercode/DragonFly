using DragonFly.Models;
using GraphQL.Types;
using System;
using DragonFly.Content;

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
