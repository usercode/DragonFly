using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Attributes
{
    /// <summary>
    /// ContentSchemaAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ContentSchemaAttribute : Attribute
    {
        public ContentSchemaAttribute()
        {

        }

        public ContentSchemaAttribute(string schemaName)
        {
            SchemaName = schemaName;
        }

        /// <summary>
        /// SchemaName
        /// </summary>
        public string? SchemaName { get; set; }
    }
}
