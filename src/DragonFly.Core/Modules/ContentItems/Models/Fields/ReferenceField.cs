// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ReferenceField
/// </summary>
[Field]
[FieldOptions(typeof(ReferenceFieldOptions))]
[FieldQuery(typeof(ReferenceFieldQuery))]
public partial class ReferenceField
{
    public const string IdField = "Id";
    public const string SchemaField = "Schema";

    public ReferenceField()
    {
    }

    public ReferenceField(ContentItem? contentItem)
    {
        ContentItem = contentItem;
    }

    private ContentItem? _contentItem;

    /// <summary>
    /// ContentItem
    /// </summary>
    public ContentItem? ContentItem
    {
        get => _contentItem;
        set
        {
            if (value?.IsNew() == true)
            {
                throw new Exception("It's not allowed to assign an unsaved content item.");
            }

            _contentItem = value;
        }
    }

    public override void Clear()
    {
        ContentItem = null;
    }

    public override string ToString()
    {
        if (ContentItem == null)
        {
            return "no reference";
        }

        return ContentItem.ToString();
    }
}
