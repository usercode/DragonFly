// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using FFMpegCore;

namespace DragonFly.AspNetCore;

/// <summary>
/// VideoProcessing
/// </summary>
public class VideoProcessing : IAssetProcessing
{
    public bool CanUse(string mimeType)
    {
        return mimeType switch
        {
            MimeTypes.Mp4 => true,
            MimeTypes.Ogg => true,
            MimeTypes.WebM => true,
            _ => false
        };
    }

    public async Task<bool> OnAssetChangedAsync(IAssetProcessingContext context)
    {
        using Stream stream = await context.OpenAssetStreamAsync();

        IMediaAnalysis mediaInfo = FFProbe.Analyse(stream);

        VideoMetadata metadata = new VideoMetadata();

        //video
        if (mediaInfo.PrimaryVideoStream != null)
        {
            metadata.VideoInfo = new VideoInfo()
            {
                Codec = mediaInfo.PrimaryVideoStream.CodecLongName,
                Width = mediaInfo.PrimaryVideoStream.Width,
                Height = mediaInfo.PrimaryVideoStream.Height,
                Duration = mediaInfo.PrimaryVideoStream.Duration,
                Fps = (int)mediaInfo.PrimaryVideoStream.FrameRate
            };
        }

        //audio
        if (mediaInfo.PrimaryAudioStream != null)
        {
            metadata.AudioInfo = new AudioInfo()
            {
                Codec = mediaInfo.PrimaryAudioStream.CodecLongName,
                ChannelLayout = mediaInfo.PrimaryAudioStream.ChannelLayout,
                Channels = mediaInfo.PrimaryAudioStream.Channels,
                Duration = mediaInfo.PrimaryAudioStream.Duration
            };
        }

        await context.SetMetadataAsync(metadata);

        return true;
    }
}
