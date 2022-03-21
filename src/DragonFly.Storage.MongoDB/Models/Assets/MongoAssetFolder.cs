using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Assets;

public class MongoAssetFolder : MongoContentBase
{
    public string? Name { get; set; }

    public Guid? Parent { get; set; }
}
