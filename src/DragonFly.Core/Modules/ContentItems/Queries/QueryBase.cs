﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class QueryBase
{
    public QueryBase()
    {
        Skip = 0;
        Top = 50;
        Pattern = string.Empty;
    }

    public int Skip { get; set; }

    public int Top { get; set; }

    public string Pattern { get; set; }
}
