using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    public interface IContentElementWithSchema : IContentElement
    {
        ContentSchema Schema { get; }
    }
}
