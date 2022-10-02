// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Content;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.Abstractions;

/// <summary>
/// FieldSerializer
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public abstract class FieldSerializer<TContentField> : IFieldSerializer
    where TContentField : IContentField
{
    public Type FieldType => typeof(TContentField);

    public abstract TContentField Read(SchemaField schemaField, BsonValue bsonValue);

    public abstract BsonValue Write(TContentField contentField);

    public BsonValue Write(IContentField contentField)
    {
        return Write((TContentField)contentField);
    }

    IContentField IFieldSerializer.Read(SchemaField definition, BsonValue bsonvalue)
    {
        return Read(definition, bsonvalue);
    }
}
