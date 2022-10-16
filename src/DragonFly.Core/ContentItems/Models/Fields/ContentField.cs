// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentItems.Models.Validations;

namespace DragonFly;

/// <summary>
/// ContentField
/// </summary>
public abstract class ContentField : IContentField
{
    public ContentField()
    {

    }

    public virtual bool CanSorting => false;

    public virtual void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
    }
}
