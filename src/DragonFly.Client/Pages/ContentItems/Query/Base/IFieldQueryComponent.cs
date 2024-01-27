// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public interface IFieldQueryComponent : IComponent
{
    SchemaField Field { get; }

    FieldQuery Query { get; }

    Type QueryType { get; }
}
