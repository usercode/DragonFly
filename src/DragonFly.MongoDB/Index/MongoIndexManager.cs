﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;
using DragonFly.MongoDB.Index;

namespace DragonFly.MongoDB;

/// <summary>
/// FieldIndexManager
/// </summary>
public sealed class MongoIndexManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static MongoIndexManager Default { get; } = new MongoIndexManager();

    public MongoIndexManager()
    {
        Fields = new Dictionary<Type, FieldIndex>();
    }

    public IDictionary<Type, FieldIndex> Fields { get; set; }

    public void Register<TFieldIndex>()
        where TFieldIndex : FieldIndex, new()
    {
        TFieldIndex index = new TFieldIndex();

        Fields[index.FieldType] = index;
    }

    public bool TryGetByType(Type fieldType, [NotNullWhen(true)] out FieldIndex? fieldIndex)
    {
        if (Fields.TryGetValue(fieldType, out var result))
        {
            fieldIndex = result;

            return true;
        }

        fieldIndex = null;

        return false;
    }
}
