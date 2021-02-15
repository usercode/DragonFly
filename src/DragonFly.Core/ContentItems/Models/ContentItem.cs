using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content;
using DragonFly.Contents.Content.Fields;
using DragonFly.ContentTypes;
using DragonFly.Core.ContentItems.Models.Validations;
using DragonFly.Data;

namespace DragonFly.Models
{
    /// <summary>
    /// ContentItem
    /// </summary>
    public class ContentItem : ContentBase, IContentItem
    {
        public ContentItem()
        {
            _fields = new ContentFields();
        }

        public ContentItem(Guid id, ContentSchema schema)
            : this()
        {
            _id = id;
            _schema = schema;
        }

        /// <summary>
        /// SchemaVersion
        /// </summary>
        public int SchemaVersion { get; set; }

        private ContentSchema _schema;

        /// <summary>
        /// Type
        /// </summary>
        public virtual ContentSchema Schema { get => _schema; set => _schema = value; }

        private ContentFields _fields;

        /// <summary>
        /// Fields
        /// </summary>
        public virtual ContentFields Fields { get => _fields; set => _fields = value; }

        public virtual bool Validate()
        {
            List<ValidationError> result = new List<ValidationError>();

            foreach(var field in Fields)
            {
                result.AddRange(field.Value.Validate());
            }

            return result.Any() == false;
        }
    }
}
