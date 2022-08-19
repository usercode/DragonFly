using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Proxies;

public interface IContentItemProxy
{
    ContentItem ContentItem { get; }
}
