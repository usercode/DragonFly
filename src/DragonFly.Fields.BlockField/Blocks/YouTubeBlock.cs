using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Fields.BlockField;

public class YouTubeBlock : Block
{
    public override string CssIcon => "bi bi-youtube";

    public string? VideoId { get; set; }

}
