// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public abstract class FieldQueryComponent<TQuery> : ComponentBase, IFieldQueryComponent
    where TQuery : FieldQuery
{
    public SchemaField Field { get; set; }

    [Parameter]
    public TQuery Query { get; set; }

    FieldQuery IFieldQueryComponent.Query => Query;

    Type IFieldQueryComponent.QueryType => typeof(TQuery);
}
