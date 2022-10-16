// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Razor.Pages.ContentSchemas.Fields;

public interface IFieldOptionsComponent : IComponent
{
    public ContentFieldOptions Options { get; }
}
