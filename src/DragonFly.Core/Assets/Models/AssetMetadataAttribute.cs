using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

[AttributeUsage(AttributeTargets.Class)]
public class AssetMetadataAttribute : Attribute
{
    public AssetMetadataAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
