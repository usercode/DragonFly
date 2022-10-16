// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

public class YouTubeBlock : Block
{
    public override string CssIcon => "fa-brands fa-youtube";

    public string? VideoId { get; set; }

}
