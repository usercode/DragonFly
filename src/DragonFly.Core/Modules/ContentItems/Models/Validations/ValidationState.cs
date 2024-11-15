// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ValidationState
/// </summary>
public class ValidationState
{
    /// <summary>
    /// Errors
    /// </summary>
    public IList<ValidationError> Errors { get; set; } = [];

    /// <summary>
    /// Result
    /// </summary>
    public ValidationResult Result { get; set; } = ValidationResult.Unknown;

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
