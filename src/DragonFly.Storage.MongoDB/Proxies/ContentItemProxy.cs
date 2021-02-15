using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using DragonFly.Data.Proxies;
using DragonFly.ContentTypes;
using DragonFly.Contents.Assets;

namespace DragonFly.Data
{
    public class ContentItemProxy
    {
        internal static ProxyGenerator Generator = new ProxyGenerator();

        public static ContentItem CreateContentItem(Guid id, string schema)
        {
            ContentItem contentItem = new ContentItem();

            return Generator.CreateClassProxyWithTarget(contentItem, new ContenItemInterceptor(id, CreateContentSchema(schema)));
        }

        public static ContentSchema CreateContentSchema(string schema)
        {
            ContentSchema contentSchema = new ContentSchema(schema);

            return Generator.CreateClassProxyWithTarget(contentSchema, new ContentSchemaInterceptor(schema));
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
}
