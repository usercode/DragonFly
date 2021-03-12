using DragonFly.Content;
using DragonFly.Data.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields
{
    /// <summary>
    /// IFieldSerializer
    /// </summary>
    public interface IFieldSerializer
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="bsonvalue"></param>
        /// <returns></returns>
        void Read(ContentSchemaField schemaField, ContentField contentField, BsonValue bsonvalue);

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="contentField"></param>
        /// <returns></returns>
        BsonValue Write(ContentField contentField);
    }
}
