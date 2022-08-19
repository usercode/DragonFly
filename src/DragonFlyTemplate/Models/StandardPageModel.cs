using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[Schema("StandardPage")]
public class StandardPageModel
{
    [Field]
    public virtual StringField Title { get; set; }

    [Field]
    public virtual BlockField Blocks { get; set; }

}
