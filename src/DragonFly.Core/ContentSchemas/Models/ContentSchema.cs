using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content;
using DragonFly.Contents.Content.Fields;
using DragonFly.Contents.Content.Schemas;
using DragonFly.Core.Content.ContentTypes;
using DragonFly.Core.ContentItems.Queries;
using DragonFly.Data.Content.ContentTypes;

namespace DragonFly.Models
{
    /// <summary>
    /// ContentSchema
    /// </summary>
    public class ContentSchema : ContentBase, IContentSchema
    {
        public ContentSchema()
        {
            _fields = new ContentSchemaFields();
            _listFields = new List<string>();
            _referenceFields = new List<string>();
            _orderFields = new List<FieldOrder>();
        }

        public ContentSchema(string name)
            : this()
        {
            _name = name;
        }

        private string _name;

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get => _name; set => _name = value; }

        private ContentSchemaFields _fields;

        /// <summary>
        /// Fields
        /// </summary>
        public virtual ContentSchemaFields Fields { get => _fields; set => _fields = value; }

        private IList<string> _listFields;

        /// <summary>
        /// ListFields
        /// </summary>
        public virtual IList<string> ListFields { get => _listFields; set => _listFields = value; }

        private IList<string> _referenceFields;

        /// <summary>
        /// ReferenceFields
        /// </summary>
        public virtual IList<string> ReferenceFields { get => _referenceFields; set => _referenceFields = value; }

        private IList<FieldOrder> _orderFields;

        /// <summary>
        /// OrderFields
        /// </summary>
        public IList<FieldOrder> OrderFields { get => _orderFields; set => _orderFields = value; }
    }
}
