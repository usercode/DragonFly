﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Query;

/// <summary>
/// ReferenceFieldQueryExtensions
/// </summary>
public static class ReferenceFieldQueryExtensions
{
    public static ContentItemQuery AddReferenceQuery(this ContentItemQuery queryParameters, string name, Guid? id)
    {
        queryParameters.Fields.Add(new ReferenceFieldQuery() { FieldName = name, ContentItemId = id });

        return queryParameters;
    }
}
