using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    /// <summary>
    /// ISchemaElement
    /// </summary>
    public interface ISchemaElement
    {
        /// <summary>
        /// Fields
        /// </summary>
        SchemaFields Fields { get; }
    }
}
