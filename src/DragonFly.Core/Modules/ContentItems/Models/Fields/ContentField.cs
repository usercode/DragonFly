// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// ContentField
/// </summary>
public abstract class ContentField
{
    public ContentField()
    {

    }

    public virtual bool CanSorting => false;

    public virtual void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
    }

    public virtual void Clear()
    {
    }
}
