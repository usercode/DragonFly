using DragonFly.Fields.BlockField.Storage.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField;

public static class Extensions
{
    public static async Task<Document?> DeserialzeAsync(this BlockField blockField)
    {
        return await new DocumentSerializer().DeserializeAsync(blockField.Value);
    }
}
