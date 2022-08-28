using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[ContentItem("BlogPost")]
public class BlogPostModel
{
    [DateField(Required = true)]
    public virtual DateTime? Date { get; set; }

    [StringField(Required = true, Index = true, ListField = true, MinLength = 8, MaxLength = 512)]
    public virtual string Title { get; set; }

    [TextField]
    public virtual string ShortDescription { get; set; }

    [SlugField(Required = true, Index = true)]
    public virtual string Slug { get; set; }

    [AssetField(ListField = true, ShowPreview = true)]
    public virtual AssetField Image { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }
}
