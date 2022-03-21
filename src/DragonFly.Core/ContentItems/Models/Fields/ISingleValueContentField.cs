using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content;

public interface ISingleValueContentField : IContentField
{
    object? Value { get; }

    bool HasValue { get; }
}
