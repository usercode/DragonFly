using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[Schema("BlogEntry")]
public class BlogEntryModel
{
    [DateField(isRequired: true)]
    public virtual DateTime? Date { get; set; }

    [StringField(isRequired: true)]
    public virtual string Title { get; set; }

    [TextField]
    public virtual string ShortDescription { get; set; }

    [SlugField(isRequired: true)]
    public virtual string Slug { get; set; }

    [AssetField]
    public virtual AssetField Image { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }
}
