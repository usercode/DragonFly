// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Validations;

/// <summary>
/// ValidationError
/// </summary>
public class ValidationError
{
    public ValidationError()
    {

    }

    public ValidationError(string field, string message)
    {
        Field = field;
        Message = message;
    }

    /// <summary>
    /// Field
    /// </summary>
    public string? Field { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string? Message { get; set; }
}
