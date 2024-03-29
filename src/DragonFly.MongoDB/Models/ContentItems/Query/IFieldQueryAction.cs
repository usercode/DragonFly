﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly.MongoDB;

/// <summary>
/// IFieldQueryAction
/// </summary>
public interface IFieldQueryAction
{
    void Apply(FieldQuery query, FieldQueryActionContext context);
}
