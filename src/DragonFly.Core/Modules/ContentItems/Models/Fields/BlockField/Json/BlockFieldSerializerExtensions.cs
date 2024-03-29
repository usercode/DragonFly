﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

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
