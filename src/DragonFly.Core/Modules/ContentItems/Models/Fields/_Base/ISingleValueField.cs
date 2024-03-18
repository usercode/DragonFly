// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Diagnostics.CodeAnalysis;

namespace DragonFly;

public interface ISingleValueField
{   
    object? Value { get; set; }

    [MemberNotNull(nameof(Value))]
    bool HasValue { get; }
}
