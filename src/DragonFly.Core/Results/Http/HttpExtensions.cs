// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http.Json;
using System.Text.Json;

namespace SmartResults;

public static class HttpExtensions
{
    public static async Task<Result> ToResultAsync(this Task<HttpResponseMessage> responseTask)
    {
        return await (await responseTask).ToResultAsync();
    }

    public static async Task<Result> ToResultAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return Result.Ok();
        }
        else
        {
            HttpErrorMessage? error = await response.Content.ReadFromJsonAsync<HttpErrorMessage>();

            ArgumentNullException.ThrowIfNull(error);

            return Result.Failed(error);
        }
    }

    public static async Task<Result<T>> ToResultAsync<T>(this Task<HttpResponseMessage> responseTask, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        return await (await responseTask).ToResultAsync<T>();
    }

    public static async Task<Result<T>> ToResultAsync<T>(this HttpResponseMessage response, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        if (response.IsSuccessStatusCode)
        {
            T? result = await response.Content.ReadFromJsonAsync<T>(jsonSerializerOptions);

            ArgumentNullException.ThrowIfNull(result);

            return Result.Ok(result);
        }
        else
        {
            HttpErrorMessage? error = await response.Content.ReadFromJsonAsync<HttpErrorMessage>();

            ArgumentNullException.ThrowIfNull(error);

            return Result<T>.Failed(error);
        }
    }
}
