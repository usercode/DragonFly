﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

public interface IBlocksContent
{
    IList<Block> Blocks { get; }
}