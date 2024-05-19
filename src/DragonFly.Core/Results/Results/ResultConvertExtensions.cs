﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace SmartResults;

public static class ResultConvertExtensions
{

    /// <summary>
    /// Convert
    /// </summary>
    public static Result<TOut> Convert<TIn, TOut>(this Result<TIn> self, Func<TIn, TOut> convert)
    {
        if (self.IsSucceeded)
        {
            return Result.Ok(convert(self));
        }
        else
        {
            return Result.Failed<TOut>(self.Error);
        }
    }

    /// <summary>
    /// ConvertAsync
    /// </summary>
    public static async Task<Result<TOut>> ConvertAsync<TIn, TOut>(this Task<Result<TIn>> selfTask, Func<TIn, TOut> convert)
    {
        return (await selfTask).Convert(convert);
    }
}
