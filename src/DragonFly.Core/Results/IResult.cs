// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace Results;

public interface IResult<TResult>
{
    /// <summary>
    /// IsFailed
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailed => Error is not null;

    /// <summary>
    /// IsSucceeded
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSucceeded => Error is null;

    public IError? Error { get; }

    public static abstract TResult Ok();

    public static abstract TResult Failed(string message);

    public static abstract TResult Failed(Exception exception);

    public static abstract TResult Failed(IError error);

    
}
