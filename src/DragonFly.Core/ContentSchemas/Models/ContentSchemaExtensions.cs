using DragonFly.Contents.Content.Fields;
using DragonFly.ContentTypes;
using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Data.Content.ContentTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content.Schemas
{
    public static class ContentSchemaExtensions
    {
        public static ContentSchema AddField<T>(this ContentSchema schema, string name, int sortkey = 0)
        {
            schema.Fields.Add(name, new ContentFieldDefinition() { FieldType = ContentFieldManager.Default.GetContentFieldName(typeof(T)), SortKey = sortkey });

            return schema;
        }
    }
}
