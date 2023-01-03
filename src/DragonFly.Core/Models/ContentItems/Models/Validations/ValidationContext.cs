// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text;

namespace DragonFly.Validations;

/// <summary>
/// ValidationContext
/// </summary>
public class ValidationContext
{
    public ValidationContext()
    {
        State = ValidationState.Unknown;
    }

    public ValidationContext(ValidationState state)
        : this()
    {
        State = state;
    }

    private IList<ValidationError> _errors = new List<ValidationError>();

    /// <summary>
    /// Errors
    /// </summary>
    public virtual IEnumerable<ValidationError> Errors { get => _errors; set => _errors = value.ToList(); }

    /// <summary>
    /// State
    /// </summary>
    public ValidationState State { get; set; }

    public void AddError(ValidationError error)
    {
        State = ValidationState.Invalid;

        _errors.Add(error);
    }

    public string GetMessage()
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (ValidationError error in Errors)
        {
            stringBuilder.AppendLine(error.Message);
        }

        return stringBuilder.ToString();
    }
}
