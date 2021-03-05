using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder
{
    /// <summary>
    /// ContentSchemaBuilder
    /// </summary>
    public interface IContentSchemaBuilder
    {
        Task BuildAsync<T>();
        
    }
}
