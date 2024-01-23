// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using DragonFly.Query;

namespace DragonFly.Client;

public interface IFieldQueryComponent
{
    SchemaField Field { get; }

    FieldQuery Query { get; }

    Type QueryType { get; }
}
