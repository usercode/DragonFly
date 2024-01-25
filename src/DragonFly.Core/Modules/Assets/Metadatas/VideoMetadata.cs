// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// VideoMetadata
/// </summary>
public class VideoMetadata : AssetMetadata
{
    /// <summary>
    /// VideoInfo
    /// </summary>
    public VideoInfo? VideoInfo { get; set; }

    /// <summary>
    /// AudioInfo
    /// </summary>
    public AudioInfo? AudioInfo { get; set; }
}
