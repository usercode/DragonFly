// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
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

        if (skip.Any())
        {
            result.Skip = int.Parse(skip.First());
        }

        if (top.Any())
        {
            result.Top = int.Parse(top.First());
        }

        return result;
    }
}
