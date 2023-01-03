// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;
using System.Text.Json.Serialization;

namespace DragonFly;

/// <summary>
/// ContentItem
/// </summary>
public class ContentItem : ContentBase<ContentItem>, IContentElement
{
    public ContentItem(ContentSchema schema)
    {
        _schema = schema;
        _fields = new ContentFields();
        _validationContext = new ValidationContext();
    }

    public ContentItem(Guid id, ContentSchema schema)
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
    /// Type
    /// </summary>
    public virtual ContentSchema Schema { get => _schema; set => _schema = value; }

    private ContentFields _fields;

    /// <summary>
    /// Fields
    /// </summary>
    public virtual ContentFields Fields { get => _fields; set => _fields = value; }

    private ValidationContext _validationContext;

    /// <summary>
    /// ValidationContext
    /// </summary>
    public virtual ValidationContext ValidationContext { get => _validationContext; set => _validationContext = value; }

    /// <summary>
    /// Validates the content item.
    /// </summary>
    public virtual bool Validate()
    {
        ValidationContext validationContext = new ValidationContext(ValidationState.Valid);

        foreach (var field in Fields)
        {
            SchemaField f = Schema.Fields[field.Key];

            if (f.Options != null)
            {
                field.Value.Validate(field.Key, f.Options, validationContext);
            }
        }

        ValidationContext = validationContext;

        return validationContext.State == ValidationState.Valid;
    }
}
