using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content;
using DragonFly.Contents.Content.Fields;
using DragonFly.Contents.Content.Schemas;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Models;

namespace DragonFly.Data.Content
{
    /// <summary>
    /// ContentItemExtensions
    /// </summary>
    public static class ContentItemFieldsExtensions
    {
       public static long? GetIntegerValue(this IContentItem contentItem, string name)
        {
            IntegerField field = contentItem.GetField<IntegerField>(name);

            return field.Value;
        }

        public static bool? GetBoolValue(this IContentItem contentItem, string name)
        {
            BoolField field = contentItem.GetField<BoolField>(name);

            return field.Value;
        }
    }
}
