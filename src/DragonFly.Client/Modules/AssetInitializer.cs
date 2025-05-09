﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.Assets.Metadata;
using DragonFly.Client.Pages.Assets.Preview;
using DragonFly.Init;

namespace DragonFly.Client;

/// <summary>
/// AssetModule
/// </summary>
public class AssetInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {
        api.MainMenu().Add("Assets", "fa-regular fa-image", "asset");

        api.Metadata().Add<ImageMetadata>().WithMetadataView<ImageMetadataView>();
        api.Metadata().Add<PdfMetadata>().WithMetadataView<PdfMetadataView>();
        api.Metadata().Add<VideoMetadata>().WithMetadataView<VideoMetadataView>();

        api.AssetPreview().Add<ImagePreviewView>(MimeTypes.WebP, MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp);
        api.AssetPreview().Add<PdfPreviewView>(MimeTypes.Pdf);
        api.AssetPreview().Add<SvgPreviewView>(MimeTypes.Svg);
        api.AssetPreview().Add<VideoPreviewView>(MimeTypes.Mp4, MimeTypes.Ogg, MimeTypes.WebM);

        return Task.CompletedTask;
    }
}
