namespace SmartResults;

/// <summary>
/// Error
/// </summary>
public class Error(string message, Exception? exception = null) : IError
{
    public static Error FromException(Exception ex)
    {
        return new Error(ex.Message, ex);
    }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; } = message;

    /// <summary>
    /// Exception
    /// </summary>
    public Exception? Exception { get; } = exception;
}
