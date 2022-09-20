using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB;

/// <summary>
/// IOptionsSerializer
/// </summary>
public interface IOptionsSerializer
{
    Type OptionsType { get; }

    ContentFieldOptions Read(BsonValue bsonvalue);
    
    BsonValue Write(ContentFieldOptions options);
}
