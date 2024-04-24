// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Results;

namespace DragonFly.AspNetCore;

public static class ResultExtensions
{
    public static IResult ToHttpResult(this Result result)
    {
        if (result.IsSucceeded)
        {
            return TypedResults.Ok();
        }
        else if (result.Error is PermissionError)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }
        else
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.IsSucceeded)
        {
            if (result.Value is null)
            {
                return TypedResults.NotFound();
            }
            else
            {
                return TypedResults.Ok(result.Value);
            }
        }
        else if (result.Error is PermissionError)
        {
            return TypedResults.Forbid(authenticationSchemes: PermissionSchemeManager.GetAll());
        }
        else
        {
            return TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> ToHttpResultAsync<T>(this Task<Result<T>> result)
    {
        Result<T> r = await result;

        return r.ToHttpResult();
    }

    public static async Task<IResult> ToHttpResultAsync(this Task<Result> result)
    {
        Result r = await result;

        return r.ToHttpResult();
    }
}
