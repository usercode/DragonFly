// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB.Query;

/// <summary>
/// IFieldQueryAction
/// </summary>
public interface IFieldQueryAction
{
    void Apply(FieldQuery query, FieldQueryActionContext context);
}
