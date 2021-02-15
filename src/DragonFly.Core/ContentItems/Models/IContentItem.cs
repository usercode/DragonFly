using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content
{
    public interface IContentItem
    {
        ContentFields Fields { get; }
    }
}
