using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[Schema("Project")]
public class ProjectModel
{
    [StringField]
    public virtual StringField Title { get; set; }

    [StringField]
    public virtual StringField SubTitle { get; set; }

    [SlugField]
    public virtual SlugField Slug { get; set; }

    [AssetField]
    public virtual AssetField Image { get; set; }    

    [StringField]
    public virtual StringField Link { get; set; }

    [HtmlField]
    public virtual HtmlField Description { get; set; }



    
}
