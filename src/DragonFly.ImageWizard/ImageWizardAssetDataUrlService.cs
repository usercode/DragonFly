using DragonFly.Client.Core.Assets;
using DragonFly.Content;
using ImageWizard;
using ImageWizard.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard;

/// <summary>
/// ImageWizardAssetDataUrlService
/// </summary>
public class ImageWizardAssetDataUrlService : IAssetPreviewUrlService
{
    public ImageWizardAssetDataUrlService(IImageWizardUrlBuilder urlBuilder)
    {
        UrlBuilder = urlBuilder;
    }

    /// <summary>
    /// UrlBuilder
    /// </summary>
    private IImageWizardUrlBuilder UrlBuilder { get; }

    public string CreateImageUrl(Asset asset, int width, int height)
    {
        if (asset.IsPdf())
        {
            return UrlBuilder.Asset(asset).PageToImage(0).Resize(width, height).BuildUrl();
        }
        else if (asset.IsSVG())
        {
            return UrlBuilder.Asset(asset).BuildUrl();
        }
        else if (asset.IsImage())
        {
            return UrlBuilder.Asset(asset).Resize(width, height).BuildUrl();
        }            
        else
        {
            return "";
        }
    }
}
