// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

public interface IContentField
{
    static abstract FieldFactory Factory { get; }

    void Validate(string fieldName, FieldOptions options, ValidationContext context);

    void Clear();
}
