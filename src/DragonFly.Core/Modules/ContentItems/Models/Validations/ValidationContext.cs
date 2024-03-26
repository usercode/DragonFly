// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text;

namespace DragonFly;

/// <summary>
/// ValidationContext
/// </summary>
public class ValidationContext
{
    public ValidationContext(ContentItem contentItem)
    {
        ContentItem = contentItem;
    }

    /// <summary>
    /// ContentItem
    /// </summary>
    public ContentItem ContentItem { get; }

    /// <summary>
    /// Errors
    /// </summary>
    public IList<ValidationError> Errors { get; } = new List<ValidationError>();

    public string GetMessage()
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (ValidationError error in Errors)
        {
            stringBuilder.AppendLine(error.Message);
        }

        return stringBuilder.ToString();
    }

    public ValidationState Execute()
    {
        ValidationState state = new ValidationState();
        state.Result = (Errors.Count == 0) ? ValidationResult.Valid : ValidationResult.Invalid;
        state.Errors = Errors;
        state.Message = GetMessage();

        return state;
    }
}
