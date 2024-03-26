// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentField
/// </summary>
public abstract class ContentField
{
    public virtual void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
    }

    public virtual void Clear()
    {
    }
}
