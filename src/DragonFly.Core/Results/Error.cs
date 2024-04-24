namespace Results;

/// <summary>
/// Error
/// </summary>
public class Error(string errorCode, string message, Exception? exception = null) : IError
{
    /// <summary>
    /// ErrorCode
    /// </summary>
    public string ErrorCode { get; } = errorCode;

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; } = message;

    /// <summary>
    /// Exception
    /// </summary>
    public Exception? Exception { get; } = exception;
}
