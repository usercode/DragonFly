using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[Schema("BlogEntry")]
public class BlogEntryModel
{
    [DateField]
    public virtual DateTimeField Date { get; set; }

    [StringField(minLength : 0)]
    public virtual StringField Title { get; set; }

    [TextAreaField]
    public virtual TextField ShortDescription { get; set; }

    [SlugField]
    public virtual SlugField Slug { get; set; }

    [AssetField]
    public virtual AssetField Image { get; set; }

    [StringField]
    public virtual StringField Link { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }
}
