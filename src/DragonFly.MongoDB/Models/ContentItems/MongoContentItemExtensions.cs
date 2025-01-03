﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

static class MongoContentItemExtensions
{
    public static ContentItem ToModel(this MongoContentItem mongoContentItem, ContentSchema schema)
    {
        ContentItem contentItem = schema.CreateContent();

        contentItem.Id = mongoContentItem.Id;
        contentItem.CreatedAt = mongoContentItem.CreatedAt;
        contentItem.ModifiedAt = mongoContentItem.ModifiedAt;
        contentItem.PublishedAt = mongoContentItem.PublishedAt;
        contentItem.Version = mongoContentItem.Version;
        contentItem.SchemaVersion = mongoContentItem.SchemaVersion;

        foreach (var mongoField in mongoContentItem.Fields)
        {
            mongoField.Value.ToModelValue(mongoField.Key, contentItem, schema);
        }

        return contentItem;
    }

    public static MongoContentItem ToMongo(this ContentItem contentItem)
    {
        MongoContentItem mongoContentItem = new MongoContentItem();

        mongoContentItem.Id = contentItem.Id;
        mongoContentItem.CreatedAt = contentItem.CreatedAt;
        mongoContentItem.ModifiedAt = contentItem.ModifiedAt;
        mongoContentItem.PublishedAt = contentItem.PublishedAt;
        mongoContentItem.Version = contentItem.Version;
        mongoContentItem.Fields = contentItem.Fields.ToMongo();
        mongoContentItem.SchemaVersion = contentItem.Schema.Version;

        return mongoContentItem;
    }

    public static MongoContentFields ToMongo(this ContentFields fields)
    {
        MongoContentFields result = new MongoContentFields();

        foreach (var field in fields)
        {
            result.Add(field.Key, field.Value.ToBsonValue());
        }

        return result;
    }

    public static MongoReferencedContent[] ToMongo(this ContentReference[] references)
    {
        return references
                        .Select(x => new MongoReferencedContent(x.Schema, x.Id))
                        .ToArray();
    }
}
