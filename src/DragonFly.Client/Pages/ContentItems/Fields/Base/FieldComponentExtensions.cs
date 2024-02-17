// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client;

public static class FieldComponentExtensions
{
    public static string? GetReadOnlyAttribute(this IFieldComponent fieldComponent)
    {
        if (fieldComponent.IsReadOnly)
        {
            return "readonly";
        }

        return null;
    }
}
