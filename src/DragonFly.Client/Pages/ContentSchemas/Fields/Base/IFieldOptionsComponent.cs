﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public interface IFieldOptionsComponent : IComponent
{
    public FieldOptions Options { get; }
}
