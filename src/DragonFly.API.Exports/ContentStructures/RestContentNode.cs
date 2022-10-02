// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.API.Models;

public class RestContentNode : RestContentBase
{
    public RestContentNode()
    {
    }

    public Guid? Structure { get; set; }

    public RestContentNode Parent { get; set; }
}
