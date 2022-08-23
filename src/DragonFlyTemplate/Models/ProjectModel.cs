using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;

namespace DragonFlyTemplate.Models;

[ContentItem("Project")]
public class ProjectModel
{
    [StringField(IsRequired = true)]
    public virtual string Title { get; set; }

    [StringField]
    public virtual string SubTitle { get; set; }

    [AssetField(IsRequired = true)]
    public virtual AssetField Image { get; set; }

    [StringField]
    public virtual string Link { get; set; }

    [HtmlField]
    public virtual string Description { get; set; }
    
}
