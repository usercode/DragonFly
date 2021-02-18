using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentPartItem
    /// </summary>
    public class ContentFieldItem
    {
        public ContentFieldItem(string name, ContentField contentPart)
        {
            Name = name;
            Part = contentPart;
        }

        /// <summary>
        /// ContentItem
        /// </summary>
        public IContentFieldDefinition Definition { get; }

        /// <summary>
        /// ContentId
        /// </summary>
        public Guid ContentId { get; }

        /// <summary>
        /// ContentType
        /// </summary>
        public string ContenType { get; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// ContentPart
        /// </summary>
        public ContentField Part { get; }

        public TPart As<TPart>( 
            )where TPart : ContentField
        {
            return (TPart)Part;
        }
    }
}
