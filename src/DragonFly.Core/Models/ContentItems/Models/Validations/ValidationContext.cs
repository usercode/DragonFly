// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Core.ContentItems.Models.Validations;

public class ValidationContext
{
    public ValidationContext()
    {
        Errors = new List<ValidationError>();
    }

    /// <summary>
    /// Errors
    /// </summary>
    public IList<ValidationError> Errors { get; }
}
