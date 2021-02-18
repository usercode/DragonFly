using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DragonFly.Content.ContentFields
{
    /// <summary>
    /// ContentField
    /// </summary>
    public class MongoContentFields : Dictionary<string, BsonValue>
    {
        public MongoContentFields()
        {

        }
    }
}
