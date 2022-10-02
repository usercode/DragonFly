// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.MongoDB.Index;

namespace DragonFly;

public static class DragonFlyApiExtensions
{
    public static MongoIndexManager Index(this IDragonFlyApi api)
    {
        return MongoIndexManager.Default;
    }
}
