// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Content item based on specified schema.
/// </summary>
public class ContentItem : ContentBase<ContentItem>, IContentElement, IEquatable<ContentItem>
{
    public ContentItem(ContentSchema schema)
    {
        ArgumentNullException.ThrowIfNull(schema);
        
        _schema = schema;
    }

    public ContentItem(ContentSchema schema, Guid id)
        : this(schema)
    {
        _id = id;
    }

    /// <summary>
    /// SchemaVersion
    /// </summary>
    public virtual int SchemaVersion { get; set; }

    private ContentSchema _schema;

    /// <summary>
    /// Schema
    /// </summary>
    public virtual ContentSchema Schema { get => _schema; set => _schema = value; }

    /// <summary>
    /// Fields
    /// </summary>
    public virtual ContentFields Fields { get; set; } = new ContentFields();

    /// <summary>
    /// ValidationContext
    /// </summary>
    public virtual ValidationState ValidationState { get; set; } = new ValidationState();

    /// <summary>
    /// Validates the content item.
    /// </summary>
    public virtual ValidationResult Validate()
    {
        ValidationContext validationContext = new ValidationContext(this);

        foreach (var field in Fields)
        {
            SchemaField f = Schema.Fields[field.Key];

            if (f.Options != null)
            {
                field.Value.Validate(field.Key, f.Options, validationContext);
            }
        }

        ValidationState = validationContext.Execute();

        return ValidationState.Result;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Schema.Name, Id);
    }

    public override bool Equals(ContentItem? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id && Schema.Name == other.Schema.Name;
    }

    public override string ToString()
    {
        return $"{Schema.Name}/{Id}";
    }
}
