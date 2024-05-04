// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace SmartResults;

public interface IResult<TResult>
{
    /// <summary>
    /// Is result failed?
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailed { get; }

    /// <summary>
    /// Is result succeeded?
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSucceeded { get; }

    /// <summary>
    /// Error
    /// </summary>
    public IError? Error { get; }

    /// <summary>
    /// Ok
    /// </summary>
    public static abstract TResult Ok();

    /// <summary>
    /// Failed
    /// </summary>
    public static abstract TResult Failed(IError error);
}
