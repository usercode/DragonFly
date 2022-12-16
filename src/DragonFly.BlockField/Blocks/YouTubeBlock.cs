// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

public class YouTubeBlock : Block
{
    public YouTubeBlock()
    {

    }

    public YouTubeBlock(string videoId)
    {
        VideoId = videoId;
    }

    public override string CssIcon => "fa-brands fa-youtube";

    /// <summary>
    /// VideoId
    /// </summary>
    public string? VideoId { get; set; }

}
