using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.AspNetCore.SchemaBuilder.SchemaStates;

namespace DragonFly;

public static class SchemaTypeManagerExtensions
{
    public static SchemaTypeManager SchemaTypes(this IDragonFlyApi api)
    {
        return SchemaTypeManager.Default;
    }
}
