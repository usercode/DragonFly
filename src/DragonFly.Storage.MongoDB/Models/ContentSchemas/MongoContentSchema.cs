using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content.Queries;
using DragonFly.Contents.Content;
using DragonFly.Data.Content.ContentTypes;
using MongoDB.Bson;

namespace DragonFly.ContentTypes
{
    /// <summary>
    /// ContentType
    /// </summary>
    public class MongoContentSchema : MongoContentBase
    {
        public MongoContentSchema()
        {
            Fields = new Dictionary<string, MongoContentFieldDefinition>();
            ListFields = new List<string>();
            ReferenceFields = new List<string>();
            OrderFields = new List<FieldOrder>();
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        

        /// <summary>
        /// Parts
        /// </summary>
        public IDictionary<string, MongoContentFieldDefinition> Fields { get; set; }

        /// <summary>
        /// ListFields
        /// </summary>
        public virtual IList<string> ListFields { get; set; }

        /// <summary>
        /// ReferenceFields
        /// </summary>
        public virtual IList<string> ReferenceFields { get; set; }

        /// <summary>
        /// OrderFields
        /// </summary>
        public virtual IList<FieldOrder> OrderFields { get; set; }
    }
}
