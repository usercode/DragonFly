// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Results;

namespace DragonFly.Core.Results.Http;

public class HttpErrorMessage : IError
{
    public string Message { get; set; } = string.Empty;
}
