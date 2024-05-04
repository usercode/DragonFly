// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace SmartResults;

public static class ResultMatchExtensions
{
    /// <summary>
    /// Match
    /// </summary>
    public static void Match(this Result self, Action succeeded, Action<IError> failed)
    {
        if (self.IsSucceeded)
        {
            succeeded();
        }
        else
        {
            failed(self.Error);
        }
    }

    /// <summary>
    /// Match
    /// </summary>
    public static TResult Match<TResult>(this Result self, Func<TResult> succeeded, Func<IError, TResult> failed)
    {
        if (self.IsSucceeded)
        {
            return succeeded();
        }
        else
        {
            return failed(self.Error);
        }
    }

    /// <summary>
    /// Match
    /// </summary>
    public static void Match<T>(this Result<T> self, Action<T> succeeded, Action<IError> failed)
    {
        if (self.IsSucceeded)
        {
            succeeded(self.Value);
        }
        else
        {
            failed(self.Error);
        }
    }

    /// <summary>
    /// Match
    /// </summary>
    public static TResult Match<T, TResult>(this Result<T> self, Func<T, TResult> succeeded, Func<IError, TResult> failed)
    {
        if (self.IsSucceeded)
        {
            return succeeded(self.Value);
        }
        else
        {
            return failed(self.Error);
        }
    }
}
