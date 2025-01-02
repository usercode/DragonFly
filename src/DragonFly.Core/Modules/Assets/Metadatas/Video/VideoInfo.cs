// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// VideoInfo
/// </summary>
public class VideoInfo
{
    /// <summary>
    /// Codec
    /// </summary>
    public string Codec { get; set; } = string.Empty;

    /// <summary>
    /// Width
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Height
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Frames per Second
    /// </summary>
    public int Fps { get; set; }
}
