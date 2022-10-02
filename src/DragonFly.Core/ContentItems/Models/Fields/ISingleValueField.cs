// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly;

public interface ISingleValueField : IContentField
{
    object? Value { get; set; }

    bool HasValue { get; }
}
