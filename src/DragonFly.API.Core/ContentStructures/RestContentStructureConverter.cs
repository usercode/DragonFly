// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

public static class RestContentStructureConverter
{
    public static ContentStructure ToModel(this RestContentStructure restContentItem)
    {
        ContentStructure contentSchema = new ContentStructure(restContentItem.Name);

        contentSchema.Id = restContentItem.Id;
        contentSchema.CreatedAt = restContentItem.CreatedAt;
        contentSchema.ModifiedAt = restContentItem.ModifiedAt;
        contentSchema.Version = restContentItem.Version;

        return contentSchema;
    }

    public static RestContentStructure ToRest(this ContentStructure contentSchema)
    {
        RestContentStructure restContentItem = new RestContentStructure();

        restContentItem.Id = contentSchema.Id;            
        restContentItem.CreatedAt = contentSchema.CreatedAt;
        restContentItem.ModifiedAt = contentSchema.ModifiedAt;
        restContentItem.Version = contentSchema.Version;
        restContentItem.Name = contentSchema.Name;

        return restContentItem;
    }

    public static ContentNode ToModel(this RestContentNode restContentNode)
    {
        ContentNode contentNode = new ContentNode();

        contentNode.Id = restContentNode.Id;
        contentNode.CreatedAt = restContentNode.CreatedAt;
        contentNode.ModifiedAt = restContentNode.ModifiedAt;
        contentNode.Version = restContentNode.Version;
        contentNode.Structure = restContentNode.Structure;

        if (restContentNode.Parent != null)
        {
            contentNode.Parent = restContentNode.Parent.ToModel();
        }

        return contentNode;
    }

    public static RestContentNode ToRest(this ContentNode contentNode)
    {
        RestContentNode restContentItem = new RestContentNode();

        restContentItem.Id = contentNode.Id;
        restContentItem.CreatedAt = contentNode.CreatedAt;
        restContentItem.ModifiedAt = contentNode.ModifiedAt;
        restContentItem.Version = contentNode.Version;
        restContentItem.Structure = contentNode.Structure;

        if (contentNode.Parent != null)
        {
            restContentItem.Parent = contentNode.Parent.ToRest();
        }

        return restContentItem;
    }
}
