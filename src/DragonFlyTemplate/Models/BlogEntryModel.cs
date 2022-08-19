﻿using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.Content;
using DragonFly.Fields.BlockField;

namespace DragonFlyTemplate.Models;

[Schema("BlogEntry")]
public class BlogEntryModel
{
    public virtual StringField Title { get; set; }

    public virtual BlockField Blocks { get; set; }
}