using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content.Fields
{
    public interface ISingleValueContentField
    {
        object Value { get; }

        bool HasValue { get; }
    }
}
