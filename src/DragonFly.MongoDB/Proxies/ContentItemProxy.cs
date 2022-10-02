// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace DragonFly.MongoDB.Proxies;

public class ContentItemProxy
{
    internal static ProxyGenerator Generator = new ProxyGenerator();

    public static ContentItem CreateContentItem(string schema, Guid id)
    {
        ContentSchema contentSchema = CreateContentSchema(schema);
        ContentItem contentItem = new ContentItem(id, contentSchema);

        return (ContentItem)Generator.CreateClassProxyWithTarget(
                                typeof(ContentItem),
                                contentItem, 
                                new object[] { id, contentSchema },
                                new ContenItemInterceptor());
    }

    public static ContentSchema CreateContentSchema(string schema)
    {
        ContentSchema contentSchema = new ContentSchema(schema);

        return (ContentSchema)Generator.CreateClassProxyWithTarget(
                                typeof(ContentSchema),
                                contentSchema, 
                                new object[] { schema },
                                new ContentSchemaInterceptor(schema));
    }

    public static Asset CreateAsset(Guid assetId)
    {
        return Generator.CreateClassProxyWithTarget(new Asset(), new AssetInterceptor(assetId));
    }

    public static AssetFolder CreateAssetFolder(Guid assetFolderId)
    {
        return Generator.CreateClassProxyWithTarget(new AssetFolder(), new AssetFolderInterceptor(assetFolderId));
    }
}
