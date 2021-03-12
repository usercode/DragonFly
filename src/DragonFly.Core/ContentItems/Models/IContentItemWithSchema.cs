using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    public interface IContentItemWithSchema : IContentItem
    {
        ContentSchema Schema { get; }
    }
}
