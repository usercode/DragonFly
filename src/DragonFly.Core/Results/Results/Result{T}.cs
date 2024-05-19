using System.Diagnostics.CodeAnalysis;

namespace SmartResults;

/// <summary>
/// Result
/// </summary>
public readonly struct Result<T> : IResult<Result<T>>, IEquatable<Result<T>>
{
    private Result(T value)
    {
        _value = value;
    }

    private Result(IError error)
    {
        Error = error;
    }

    private readonly T _value = default!;

    /// <summary>
    /// Gets the result value.
    /// </summary>
    public T Value
    {
        get
        {
            if (IsFailed)
            {
                throw new InvalidOperationException();
            }

            return _value;
        }
    }

    /// <summary>
    /// Error
    /// </summary>
    public IError? Error { get; }

    /// <summary>
    /// Is result failed?
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailed => Error is not null;

    /// <summary>
    /// Is result succeeded?
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSucceeded => Error is null;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Result<T> result && Equals(result);
    }

    public bool Equals(Result<T> other)
    {
        return IsSucceeded && other.IsSucceeded && EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(Result<T>), Value);
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
            return $"{Value}";
        }
    }   

    /// <summary>
    /// Creates a succeeded result.
    /// </summary>
    public static Result<T> Ok()
    {
        return new Result<T>();
    }

    /// <summary>
    /// Creates a succeeded result with value.
    /// </summary>
    public static Result<T> Ok(T value)
    {
        return new Result<T>(value);
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result<T> Failed(string message)
    {
        return new Result<T>(new Error(message));
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result<T> Failed(Exception exception)
    {
        return new Result<T>(new Error(exception.Message, exception));
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static Result<T> Failed(IError error)
    {
        return new Result<T>(error);
    }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator T(Result<T> result)
    {
        if (result.IsFailed)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    public static implicit operator Result(Result<T> result)
    {
        return result.IsSucceeded ? Result.Ok() : Result.Failed(result.Error);
    }

    public static bool operator ==(Result<T> left, Result<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Result<T> left, Result<T> right)
    {
        return !(left == right);
    }    
}
