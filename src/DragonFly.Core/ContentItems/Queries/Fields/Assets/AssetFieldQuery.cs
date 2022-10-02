// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Query;

/// <summary>
/// AssetFieldQuery
/// </summary>
public class AssetFieldQuery : FieldQuery
{
    /// <summary>
    /// AssetId
    /// </summary>
    public Guid? AssetId { get; set; }

    public override bool IsEmpty() => AssetId == null;
}
