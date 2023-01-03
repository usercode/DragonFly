// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Validations;

/// <summary>
/// ValidationError
/// </summary>
public class ValidationError
{
    public ValidationError()
    {

    }

    public ValidationError(string field, string message)
    {
        Field = field;
        Message = message;
    }

    /// <summary>
    /// Field
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; }

    public override string ToString()
    {
        return Message;
    }
}
