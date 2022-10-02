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
/// StringFieldQueryType
/// </summary>
public enum StringFieldQueryType
{
    Equals,
    Contains,
    StartsWith,
    EndsWith
}
