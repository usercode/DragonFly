// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using SmartResults;

namespace DragonFly.AspNetCore;

public static class ResultExtensions
{
    public static IResult ToHttpResult<TResult>(this TResult result)
        where TResult : IResult<TResult>
    {
        int statusCode = StatusCodes.Status200OK;

        if (result.IsFailed)
        {
            if (result.Error is PermissionError)
            {
                statusCode = StatusCodes.Status403Forbidden;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
            }
        }

        return TypedResults.Json(result, statusCode: statusCode);
    }

    public static async Task<IResult> ToHttpResultAsync<TResult>(this Task<TResult> result)
        where TResult : IResult<TResult>
    {
        TResult r = await result.ConfigureAwait(false);

        return r.ToHttpResult();
    }
}
