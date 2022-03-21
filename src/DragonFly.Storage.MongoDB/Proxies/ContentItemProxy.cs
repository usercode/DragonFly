using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using DragonFly.Data.Proxies;
using DragonFly.ContentTypes;
using DragonFly.Contents.Assets;
using DragonFly.Content;

namespace DragonFly.Data;

public class ContentItemProxy
{
    internal static ProxyGenerator Generator = new ProxyGenerator();

    public static ContentItem CreateContentItem(Guid id, string schema)
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
