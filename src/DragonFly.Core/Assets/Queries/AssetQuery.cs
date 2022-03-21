using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Assets.Queries;

public class AssetQuery
{
    public AssetQuery()
    {
        Pattern = string.Empty;
        Take = 50;
    }

    public int Skip { get; set; }

    public int Take { get; set; }

    public string Pattern { get; set; }

    public Guid? Folder { get; set; }
}
