// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DragonFly.Assets.ImageSharp;

public class CropWhitespacesAssetAction : IAssetAction
{
    public string Name => "CropWhitespaces";

    public async Task<bool> ProcessAsync(IAssetActionContext context)
    {
        Image<Rgb24> image;

        using (Stream input = await context.OpenReadAsync().ConfigureAwait(false))
        {
            image = await Image.LoadAsync<Rgb24>(input).ConfigureAwait(false);
        }

        int top = 0;
        int bottom = 0;
        int left = 0;
        int right = 0;

        image.ProcessPixelRows(p =>
        {
            Rgb24 whitePixel = new Rgb24(255, 255, 255);

            //top
            for (int i = 0; i < p.Height; i++)
            {
                Span<Rgb24> row = p.GetRowSpan(i);

                bool foundNonWhite = row.ContainsAnyExcept(whitePixel);

                if (foundNonWhite)
                {
                    top = i;

                    break;
                }
            }

            //bottom
            for (int i = image.Height - 1; i > top; i--)
            {
                Span<Rgb24> row = p.GetRowSpan(i);

                bool foundNonWhite = row.ContainsAnyExcept(whitePixel);

                if (foundNonWhite)
                {
                    bottom = image.Height - i - 1;
                    break;
                }
            }

            bool foundSide = false;

            //sides
            for (int i = top; i < image.Height - bottom; i++)
            {
                Span<Rgb24> row = p.GetRowSpan(i);

                //left
                int found = row.IndexOfAnyExcept(whitePixel);

                if (found == -1)
                {
                    continue;
                }

                if (foundSide)
                {
                    left = Math.Min(left, found);
                }
                else
                {
                    left = found;
                }

                //right
                found = row.LastIndexOfAnyExcept(whitePixel);

                if (found == -1)
                {
                    throw new Exception();
                }

                found = image.Width - found - 1;

                if (foundSide)
                {
                    right = Math.Min(right, found);
                }
                else
                {
                    right = found;
                }

                foundSide = true;
            }
        });

        if (top == 0 && bottom == 0 && left == 0 && right == 0)
        {
            return false; //no whitespaces found
        }

        if (top == image.Height)
        {
            return false;
        }

        image.Mutate(x => x.Crop(new Rectangle(left, top, image.Width - left - right, image.Height - bottom - top)));

        using Stream output = await context.OpenWriteAsync().ConfigureAwait(false);

        await image.SaveAsync(output, image.Metadata.DecodedImageFormat!).ConfigureAwait(false);

        return true;
    }
}
