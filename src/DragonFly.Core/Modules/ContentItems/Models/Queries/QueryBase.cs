// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class QueryBase
{
    public QueryBase()
    {
        Skip = 0;
        Take = 50;
        Pattern = string.Empty;
    }

    public int Skip { get; set; }

    public int Take { get; set; }

    public string Pattern { get; set; }
}
