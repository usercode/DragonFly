using System.Diagnostics.CodeAnalysis;

namespace SmartResults;

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
    /// Is result failed?
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailed => Error != null;

    /// <summary>
    /// Is result is succeeded?
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
        return HashCode.Combine(typeof(Result), IsSucceeded);
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
        else
        {
            return $"";
        }
    }

    /// <summary>
    /// Creates a succeeded result.
    /// </summary>
    public static Result Ok()
    {
        return new Result();
    }

    /// <summary>
    /// Creates a succeeded result with value.
    /// </summary>
    public static Result<T> Ok<T>(T value = default!)
    {
        return Result<T>.Ok(value);
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result Failed(string message)
    {
        return Failed(new Error(message));
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result Failed(Exception exception)
    {
        return Failed(new Error(exception.Message, exception));
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result Failed(IError error)
    {
        return new Result(error);
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result<T> Failed<T>(string message)
    {
        return Result<T>.Failed(message);
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result<T> Failed<T>(IError error)
    {
        return Result<T>.Failed(error);
    }

    public static TResult Try<TResult>(Func<TResult> action)
        where TResult : IResult<TResult>
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            return TResult.Failed(SmartResults.Error.FromException(ex));
        }
    }

    public static async Task<TResult> TryAsync<TResult>(Func<Task<TResult>> action)
        where TResult : IResult<TResult>
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            return TResult.Failed(SmartResults.Error.FromException(ex));
        }
    }

    public static Result OkIf(bool isSucceeded, Func<IError> error)
    {
        return isSucceeded ? Ok() : Failed(error());
    }

    public static Result FailedIf(bool isFailed, Func<IError> error)
    {
        return isFailed ? Failed(error()) : Ok();
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
