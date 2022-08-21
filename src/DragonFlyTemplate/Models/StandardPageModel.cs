using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[Schema("StandardPage")]
public class StandardPageModel
{
    [StringField]
    public virtual StringField Title { get; set; }

    [SlugField]
    public virtual SlugField Slug { get; set; }

    [BlockField]
    public virtual BlockField MainContent { get; set; }

}
