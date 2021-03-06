﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    public abstract class SingleValueContentFieldOptions<T> : ContentFieldOptions
        where T : struct
    { 

        public T? DefaultValue { get; set; }
    }
}
