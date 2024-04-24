using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Results;

/// <summary>
/// Result
/// </summary>
public readonly struct Result : IResult<Result>, IEquatable<Result>
{
    private Result(IError error)
    {
        Error = error;
    }

    /// <summary>
    /// Error
    /// </summary>
    public IError? Error { get; }

    /// <summary>
    /// IsFailed
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailed => Error != null;

    /// <summary>
    /// IsSucceeded
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSucceeded => Error == null;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Result result && Equals(result);
    }

    public bool Equals(Result other)
    {
        return IsSucceeded == other.IsSucceeded;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(Result));
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        if (IsFailed)
        {
            return $"Error: {Error}";
        }

        return string.Empty;
    }

    /// <summary>
    /// Match
    /// </summary>
    public void Match(Action succeeded, Action<IError> failed)
    {
        if (IsSucceeded)
        {
            succeeded();
        }
        else
        {
            failed(Error);
        }
    }

    /// <summary>
    /// Match
    /// </summary>
    public TResult Match<TResult>(Func<TResult> succeeded, Func<IError, TResult> failed)
    {
        if (IsSucceeded)
        {
            return succeeded();
        }
        else
        {
            return failed(Error);
        }
    }

    /// <summary>
    /// Ok
    /// </summary>
    public static Result Ok()
    {
        return new Result();
    }

    /// <summary>
    /// Ok
    /// </summary>
    public static Result<T> Ok<T>()
    {
        return Result<T>.Ok();
    }

    /// <summary>
    /// Ok
    /// </summary>
    public static Result<T> Ok<T>(T value)
    {
        return Result<T>.Ok(value);
    }

    /// <summary>
    /// Failed
    /// </summary>
    public static Result Failed(string message)
    {
        return Failed(new Error("", message));
    }

    /// <summary>
    /// Failed
    /// </summary>
    public static Result Failed(Exception exception)
    {
        return Failed(new Error("", exception.Message));
    }

    /// <summary>
    /// Failed
    /// </summary>
    public static Result Failed(IError error)
    {
        return new Result(error);
    }

    public static Result OkIf(bool isSucceded, string error)
    {
        return isSucceded ? Ok() : Failed(error);
    }

    public static Result FailedIf(bool isFailed, string error)
    {
        return isFailed ? Failed(error) : Ok();
    }

    public static bool operator ==(Result left, Result right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Result left, Result right)
    {
        return !(left == right);
    }    
}
