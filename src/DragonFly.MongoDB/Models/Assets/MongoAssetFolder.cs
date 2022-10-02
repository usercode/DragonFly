// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.MongoDB;

public class MongoAssetFolder : MongoContentBase
{
    public string? Name { get; set; }

    public Guid? Parent { get; set; }
}
