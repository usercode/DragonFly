// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.BlockField;

namespace DragonFly.BlockField.Razor;

public interface IBlockComponent
{
    Block Block { get; }

}
