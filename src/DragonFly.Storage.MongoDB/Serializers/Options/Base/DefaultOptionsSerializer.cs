using DragonFly.Content;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Serializers.Options.Base
{
    /// <summary>
    /// DefaultOptionsSerializer
    /// </summary>
    internal class DefaultOptionsSerializer<TOptions> : OptionsSerializer<TOptions>
        where TOptions : ContentFieldOptions, new()
    {
        public override TOptions Read(BsonValue bsonValue)
        {
            if (bsonValue is BsonDocument bsonDocument)
            {
                return BsonSerializer.Deserialize<TOptions>(bsonDocument);
            }

            return new TOptions();
        }

        public override BsonValue Write(TOptions options)
        {
            return options.ToBsonDocument(options.GetType());
        }
    }
}
