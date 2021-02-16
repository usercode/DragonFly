using DragonFly.Models;
using DragonFly.Data.Content.ContentTypes;
using GraphQL.Types;
using System;

namespace DragonFly.AspNetCore.GraphQL
{
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
}
