using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Fields.BlockField.Storage.Serializers;

namespace DragonFly.Fields.BlockField;

public static class DocumentSerializerExtensions
{
    public static async Task<Document> GetDocumentAsync(this BlockField blockField)
    {
        return await DocumentSerializer.DeserializeAsync(blockField.Value);
    }

    public static async Task SetDocumentAsync(this BlockField blockField, Document document)
    {
        blockField.Value = await DocumentSerializer.SerializeAsync(document);
    }
}
