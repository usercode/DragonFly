using DragonFly.Client.Core.Assets;
using DragonFly.Content;
using ImageWizard;
using ImageWizard.Client;

namespace DragonFly.ImageWizard;

/// <summary>
/// ImageWizardAssetUrlService
/// </summary>
public class ImageWizardAssetUrlService : IAssetPreviewUrlService
{
    public ImageWizardAssetUrlService(IImageWizardUrlBuilder urlBuilder)
    {
        UrlBuilder = urlBuilder;
    }

    /// <summary>
    /// UrlBuilder
    /// </summary>
    private IImageWizardUrlBuilder UrlBuilder { get; }

    public string CreateUrl(Asset asset, int width, int height)
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
