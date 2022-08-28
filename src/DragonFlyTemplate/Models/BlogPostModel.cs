using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;
using UglyToad.PdfPig.Fonts.TrueType.Names;

namespace DragonFlyTemplate.Models;

[ContentItem("BlogPost")]
public class BlogPostModel
{
    [DateField(IsRequired = true)]
    public virtual DateTime? Date { get; set; }

    [StringField(IsRequired = true, Index = true, MinLength = 8, MaxLength = 512)]
    public virtual string Title { get; set; }

    [TextField]
    public virtual string ShortDescription { get; set; }

    [SlugField(IsRequired = true, Index = true)]
    public virtual string Slug { get; set; }

    [AssetField]
    public virtual AssetField Image { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }
}
