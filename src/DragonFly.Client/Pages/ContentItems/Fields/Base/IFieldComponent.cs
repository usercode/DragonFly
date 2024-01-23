// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public interface IFieldComponent : IComponent
{
    Type FieldType { get; }

    ContentField Field { get; }

    FieldOptions Options { get; }
}
