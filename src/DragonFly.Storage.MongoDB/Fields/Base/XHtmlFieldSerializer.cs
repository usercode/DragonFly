using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Fields.Base
{
    public class XHtmlFieldSerializer : FieldSerializer<XHtmlField>
    {
        public override void Read(ContentSchemaField definition, XHtmlField contentField, BsonValue bsonvalue)
        {
            if (bsonvalue is BsonString bsonString)
            {
                contentField.Value = bsonString.Value;
            }
        }

        public override BsonValue Write(XHtmlField contentField)
        {
            if (contentField.HasValue)
            {
                return contentField.Value;
            }
            else
            {
                return BsonNull.Value;
            }
        }
    }
}
