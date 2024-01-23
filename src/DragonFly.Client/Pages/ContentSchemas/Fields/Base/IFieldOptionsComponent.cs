// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public interface IFieldOptionsComponent : IComponent
{
    public FieldOptions Options { get; }

    public Type OptionsType { get; }
}
