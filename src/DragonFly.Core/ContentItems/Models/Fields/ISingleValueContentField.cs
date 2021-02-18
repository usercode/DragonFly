using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    public interface ISingleValueContentField
    {
        object Value { get; }

        bool HasValue { get; }
    }
}
