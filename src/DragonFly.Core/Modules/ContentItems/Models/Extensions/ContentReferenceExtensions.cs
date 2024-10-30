// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class ContentReferenceExtensions
{
    public static ContentReference ToReference(this ContentItem content)
    {
        return new ContentReference(content.Schema.Name, content.Id);
    }

    public static ContentReference ToReference(this Asset asset)
    {
        return new ContentReference(Asset.Schema, asset.Id);
    }

    public static ContentReference[] GetReferencedContent(this ContentItem content)
    {
        HashSet<ContentReference> cache = new HashSet<ContentReference>();

        foreach (var field in content.Fields)
        {
            if (field.Value is not IReferencedContent referencedContent)
            {
                continue;
            }

            foreach (ContentReference r in referencedContent.GetReferences())
            {
                cache.Add(r);
            }
        }

        return cache.ToArray();
    }
}
