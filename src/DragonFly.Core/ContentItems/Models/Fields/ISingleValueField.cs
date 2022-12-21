// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface ISingleValueField
{
    object? Value { get; set; }

    bool HasValue { get; }
}
