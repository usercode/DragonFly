// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using Microsoft.AspNetCore.Components;

namespace DragonFly.Client;

public interface IFieldComponent : IComponent
{
    string FieldName { get; }

    bool IsReadOnly { get; }

    Type FieldType { get; }

    ContentField Field { get; }

    FieldOptions Options { get; }
}

public interface IFieldComponent<TField> : IFieldComponent
    where TField : ContentField
{
    new Type FieldType => typeof(TField);
}
