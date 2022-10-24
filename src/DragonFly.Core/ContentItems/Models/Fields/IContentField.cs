// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentItems.Models.Validations;

namespace DragonFly;

/// <summary>
/// IContentField
/// </summary>
public interface IContentField
{
    bool CanSorting { get; }

    void Clear();

    void Validate(string fieldName, ContentFieldOptions options, ValidationContext context);
}
