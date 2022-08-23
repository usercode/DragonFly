using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[ContentItem("BlogEntry")]
public class BlogEntryModel
{
    [DateField(IsRequired = true)]
    public virtual DateTime? Date { get; set; }

    [StringField(IsRequired = true)]
    public virtual string Title { get; set; }

    [TextField]
    public virtual string ShortDescription { get; set; }

    [SlugField(IsRequired = true)]
    public virtual string Slug { get; set; }

    [AssetField]
    public virtual AssetField Image { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }
}
