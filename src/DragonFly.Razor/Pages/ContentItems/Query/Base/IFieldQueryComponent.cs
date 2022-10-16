// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly.Razor.Pages.ContentItems.Query;

public interface IFieldQueryComponent
{
    SchemaField Field { get; }

    FieldQuery Query { get; }
}
