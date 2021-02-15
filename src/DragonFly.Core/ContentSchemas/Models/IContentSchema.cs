using DragonFly.Contents.Content.Fields;
using DragonFly.Data.Content.ContentTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content.Schemas
{
    public interface IContentSchema
    {
        ContentSchemaFields Fields { get; }
    }
}
