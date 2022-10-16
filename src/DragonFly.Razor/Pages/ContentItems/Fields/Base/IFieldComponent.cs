// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Razor.Pages.ContentItems.Fields;

public interface IFieldComponent : IComponent
{
    IContentField Field { get; }

    ContentFieldOptions Options { get; }
}
