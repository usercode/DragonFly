﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public interface IDragonFlyApi
{
    Task InitAsync();

    IServiceProvider ServiceProvider { get; }
}
