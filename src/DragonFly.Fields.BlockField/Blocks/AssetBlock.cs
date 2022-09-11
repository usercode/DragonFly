﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

/// <summary>
/// AssetBlock
/// </summary>
public class AssetBlock : Block
{
    public override string CssIcon => "bi bi-image";

    public Guid? AssetId { get; set; }
}
