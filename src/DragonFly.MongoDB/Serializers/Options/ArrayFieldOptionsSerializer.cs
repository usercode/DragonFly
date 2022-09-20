using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB;

/// <summary>
/// ArrayFieldOptionsSerializer
/// </summary>
public class ArrayFieldOptionsSerializer : OptionsSerializer<ArrayFieldOptions>
{
    public override ArrayFieldOptions Read(BsonValue bsonValue)
    {
        ArrayFieldOptions options = new ArrayFieldOptions();

        if (bsonValue is BsonDocument bsonDocument)
        {

        }

        return options;
    }

    public override BsonValue Write(ArrayFieldOptions options)
    {
        return options.ToMongo();
    }
}
