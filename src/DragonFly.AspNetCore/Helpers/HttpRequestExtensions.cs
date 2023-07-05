// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace DragonFly.AspNetCore;

public static class HttpRequestExtensions
{
    public static ContentQuery GetQuery(this HttpRequest request)
    {
        ContentQuery result = new ContentQuery();

        StringValues skip = request.Query["$skip"];
        StringValues top = request.Query["$top"];

        if (skip.Count > 0)
        {
            result.Skip = int.Parse(skip[0]);
        }

        if (top.Count > 0)
        {
            result.Top = int.Parse(top[0]);
        }

        return result;
    }
}
