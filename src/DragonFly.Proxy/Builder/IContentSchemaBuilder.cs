using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Proxy;

/// <summary>
/// ContentSchemaBuilder
/// </summary>
public interface IContentSchemaBuilder
{
    Task AddAsync(Type type);
    
}
