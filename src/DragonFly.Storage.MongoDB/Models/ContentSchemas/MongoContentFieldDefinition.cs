using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DragonFly.Data.Content.ContentTypes
{
    /// <summary>
    /// ContentPartDefinition
    /// </summary>
    public class MongoContentFieldDefinition
    {
        public MongoContentFieldDefinition()
        {
            Options = BsonNull.Value;
        }

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
        public BsonValue Options {get;set;}
    }
}
