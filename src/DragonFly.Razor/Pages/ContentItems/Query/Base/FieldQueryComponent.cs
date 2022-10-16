// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Razor.Pages.ContentItems.Query;

public abstract class FieldQueryComponent<TQuery> : ComponentBase, IFieldQueryComponent
    where TQuery : FieldQuery
{
    public SchemaField Field { get; set; }

    [Parameter]
    public TQuery Query { get; set; }

    FieldQuery IFieldQueryComponent.Query => Query;
}
