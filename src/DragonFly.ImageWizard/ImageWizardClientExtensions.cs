using DragonFly.Content;
using ImageWizard.Client;

namespace DragonFly.ImageWizard
{
    public static class ImageWizardClientExtensions
    {
        public static Image Asset(this ILoader loader, Asset asset)
        {
            return new Image(loader.LoadData("dragonfly", $"{asset.Id}?v={asset.Hash}"));
        }
    }
}
