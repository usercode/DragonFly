﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Core;

public abstract class QueryBase
{
    public QueryBase()
    {
        Skip = 0;
        Top = 50;
    }

    public int Skip { get; set; }

    public int Top { get; set; }
}