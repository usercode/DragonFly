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
        using Stream stream = await context.OpenAssetStreamAsync().ConfigureAwait(false);

        IMediaAnalysis mediaInfo = FFProbe.Analyse(stream);

        VideoMetadata metadata = new VideoMetadata();

        metadata.FormatName = mediaInfo.Format.FormatLongName;
        metadata.Duration = mediaInfo.Duration;

        //video
        if (mediaInfo.PrimaryVideoStream != null)
        {
            metadata.VideoInfo = new VideoInfo()
            {
                Codec = mediaInfo.PrimaryVideoStream.CodecLongName,
                Width = mediaInfo.PrimaryVideoStream.Width,
                Height = mediaInfo.PrimaryVideoStream.Height,
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
                Channels = mediaInfo.PrimaryAudioStream.Channels
            };
        }

        await context.SetMetadataAsync(metadata).ConfigureAwait(false);

        return true;
    }
}
