using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public static class ContentSchemaFieldExtensions
    {
        public static ArrayFieldOptions GetArrayFieldOptions(this SchemaField schemaField)
        {
            ArrayFieldOptions? options = (ArrayFieldOptions?)schemaField.Options;

            if (options == null)
            {
                throw new Exception();
            }

            return options;
        }
    }
}
