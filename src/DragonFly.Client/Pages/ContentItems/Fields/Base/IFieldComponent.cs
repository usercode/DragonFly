// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public interface IFieldComponent : IComponent
{
    ContentField Field { get; }

    FieldOptions Options { get; }
}
