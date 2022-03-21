using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Serializers.Options;

/// <summary>
/// OptionsSerializer
/// </summary>
public abstract class OptionsSerializer<TOptions> : IOptionsSerializer
    where TOptions : ContentFieldOptions
{
    public Type OptionsType => typeof(TOptions);

    public abstract TOptions Read(BsonValue bsonValue);
    
    public abstract BsonValue Write(TOptions options);

    BsonValue IOptionsSerializer.Write(ContentFieldOptions options)
    {
        return Write((TOptions)options);
    }

    ContentFieldOptions IOptionsSerializer.Read(BsonValue bsonvalue)
    {
        return Read(bsonvalue);
    }
}
