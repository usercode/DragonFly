// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class ValidationErrorExtensions
{
    public static ValidationContext AddRequireValidation(this ValidationContext context, string field)
    {
        context.Errors.Add(new ValidationError(field, $"The field \"{field}\" is required."));

        return context;
    }

    public static ValidationContext AddRangeValidation(this ValidationContext context, string field, double? from, double? to)
    {
        context.Errors.Add(new ValidationError(field, $"The field \"{field}\" is out of range! The value must be between {from} and {to}."));

        return context;
    }

    public static ValidationContext AddMinimumValidation(this ValidationContext context, string field, double? value)
    {
        context.Errors.Add(new ValidationError(field, $"The field \"{field}\" must be at least {value} characters long."));

        return context;
    }

    public static ValidationContext AddMaximumValidation(this ValidationContext context, string field, double? value)
    {
        context.Errors.Add(new ValidationError(field, $"The field \"{field}\" can't be longer than {value} characters."));

        return context;
    }

    public static ValidationContext AddInvalidValidation(this ValidationContext context, string field)
    {
        context.Errors.Add(new ValidationError(field, $"The field \"{field}\" isn't valid!"));

        return context;
    }
}
