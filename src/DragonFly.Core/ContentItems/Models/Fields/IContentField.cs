﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using DragonFly.Core.ContentItems.Models.Validations;

namespace DragonFly.Content;

/// <summary>
/// IContentField
/// </summary>
public interface IContentField
{
    bool CanSorting { get; }

    void Validate(string fieldName, ContentFieldOptions options, ValidationContext context);
}