// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Options;

public class GeneralOptions
{
    public GeneralOptions()
    {
        Name = "DragonFly";
    }

    public string Name { get; set; }
}
