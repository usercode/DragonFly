using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.Abstractions
{
    /// <summary>
    /// IFieldSerializer
    /// </summary>
    public interface IFieldSerializer
    {
        /// <summary>
        /// FieldType
        /// </summary>
        Type FieldType { get; }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="bsonvalue"></param>
        /// <returns></returns>
        ContentField Read(SchemaField schemaField, BsonValue bsonvalue);

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="contentField"></param>
        /// <returns></returns>
        BsonValue Write(ContentField contentField);
    }
}
