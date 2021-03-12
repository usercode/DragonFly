using DragonFly.Content;
using DragonFly.Data.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class ArrayFieldSerializer : FieldSerializer<ArrayField>
    {
        public override void Read(ContentSchemaField schemaField, ArrayField contentField, BsonValue bsonvalue)
        {
            if (bsonvalue is BsonArray bsonArray)
            {
                ArrayFieldOptions arrayOptions = schemaField.Options as ArrayFieldOptions;

                foreach (BsonDocument item in bsonArray)
                {
                    ArrayFieldItem arrayFieldItem = arrayOptions.CreateArrayField();

                    foreach (BsonElement subitem in item)
                    {
                        subitem.Value.FromBsonValue(subitem.Name, arrayFieldItem, arrayOptions);
                    }

                    contentField.Items.Add(arrayFieldItem);
                }
            }
        }

        public override BsonValue Write(ArrayField contentField)
        {
            BsonArray bsonArray = new BsonArray();

            foreach (ArrayFieldItem item in contentField.Items)
            {
                BsonDocument doc = new BsonDocument();

                foreach (var f in item.Fields)
                {
                    doc.Add(f.Key, f.Value.ToBsonValue());
                }

                bsonArray.Add(doc);
            }

            return bsonArray;
        }
    }
}
