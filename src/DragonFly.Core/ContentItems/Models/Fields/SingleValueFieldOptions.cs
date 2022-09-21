﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

public abstract class SingleValueFieldOptions<T> : ContentFieldOptions
    where T : struct
{ 

    public T? DefaultValue { get; set; }
}