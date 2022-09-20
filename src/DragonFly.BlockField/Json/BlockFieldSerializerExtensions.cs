using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;

public static class BlockFieldSerializerExtensions
{
    public static async Task<Document> GetDocumentAsync(this BlockField blockField)
    {
        return await BlockFieldSerializer.DeserializeAsync(blockField.Value);
    }

    public static async Task SetDocumentAsync(this BlockField blockField, Document document)
    {
        blockField.Value = await BlockFieldSerializer.SerializeAsync(document);
    }
}
