using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[ContentItem("Project")]
public class ProjectModel
{
    [StringField(isRequired: true)]
    public virtual StringField Title { get; set; }

    [StringField]
    public virtual StringField SubTitle { get; set; }

    [AssetField(isRequired: true)]
    public virtual AssetField Image { get; set; }    

    [StringField]
    public virtual StringField Link { get; set; }

    [HtmlField]
    public virtual HtmlField Description { get; set; }
    
}
