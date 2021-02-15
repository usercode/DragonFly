using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content.ContentParts;
using DragonFly.Core.Content.ContentTypes;
using DragonFly.Data.Content.ContentParts;

namespace DragonFly.Data.Content.ContentTypes
{
    /// <summary>
    /// ContentPartDefinition
    /// </summary>
    public class ContentFieldDefinition : IContentFieldDefinition
    {
        /// <summary>
        /// Label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// SortKey
        /// </summary>
        public int SortKey { get; set; }

        /// <summary>
        /// FieldType
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// Options
        /// </summary>
        public ContentFieldOptions Options { get; set; }
    }
}
