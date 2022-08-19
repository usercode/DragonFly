using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder.Builder;

public class ContentSchemaBuilderOptions
{
    public ContentSchemaBuilderOptions()
    {
        Types = new List<Type>();
    }

    public IList<Type> Types { get; }

    public ContentSchemaBuilderOptions AddType<T>()
        where T : class
    {
        Types.Add(typeof(T));

        return this;
    }
}
