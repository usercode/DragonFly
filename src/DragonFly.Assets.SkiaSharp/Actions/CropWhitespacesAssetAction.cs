//// Copyright (c) usercode
//// https://github.com/usercode/DragonFly
//// MIT License

//using System;
//using System.IO;
//using System.Threading.Tasks;
//using DragonFly.Assets;
//using SkiaSharp;

//namespace DragonFly.Assets;

//public class CropWhitespacesAssetAction : IAssetAction
//{
//    public string Name => "CropWhitespaces";

//    public async Task<bool> ProcessAsync(IAssetActionContext context)
//    {
//        // Read input into memory so we can inspect encoded format and decode via Skia
//        using Stream input = await context.OpenReadAsync().ConfigureAwait(false);
//        using var ms = new MemoryStream();
//        await input.CopyToAsync(ms).ConfigureAwait(false);
//        ms.Position = 0;

//        SKEncodedImageFormat encodedFormat = SKEncodedImageFormat.Png;

//        using (var codec = SKCodec.Create(ms))
//        {
//            if (codec != null)
//            {
//                encodedFormat = codec.EncodedFormat;
//            }
//        }

//        ms.Position = 0;

//        using var bitmap = SKBitmap.Decode(ms);

//        if (bitmap == null)
//        {
//            return false;
//        }

//        int top = 0;
//        int bottom = 0;
//        int left = 0;
//        int right = 0;

//        int width = bitmap.Width;
//        int height = bitmap.Height;

//        bool foundAny = false;

//        // top
//        for (int y = 0; y < height; y++)
//        {
//            bool foundNonWhite = false;

//            for (int x = 0; x < width; x++)
//            {
//                var c = bitmap.GetPixel(x, y);
//                if (!(c.Red == 255 && c.Green == 255 && c.Blue == 255))
//                {
//                    foundNonWhite = true;
//                    break;
//                }
//            }

//            if (foundNonWhite)
//            {
//                top = y;
//                foundAny = true;
//                break;
//            }
//        }

//        if (!foundAny)
//        {
//            return false; // image is completely white
//        }

//        // bottom
//        for (int y = height - 1; y > top; y--)
//        {
//            bool foundNonWhite = false;

//            for (int x = 0; x < width; x++)
//            {
//                var c = bitmap.GetPixel(x, y);
//                if (!(c.Red == 255 && c.Green == 255 && c.Blue == 255))
//                {
//                    foundNonWhite = true;
//                    break;
//                }
//            }

//            if (foundNonWhite)
//            {
//                bottom = height - y - 1;
//                break;
//            }
//        }

//        bool foundSide = false;

//        for (int y = top; y < height - bottom; y++)
//        {
//            int foundLeft = -1;
//            int foundRight = -1;

//            for (int x = 0; x < width; x++)
//            {
//                var c = bitmap.GetPixel(x, y);
//                if (!(c.Red == 255 && c.Green == 255 && c.Blue == 255))
//                {
//                    foundLeft = x;
//                    break;
//                }
//            }

//            for (int x = width - 1; x >= 0; x--)
//            {
//                var c = bitmap.GetPixel(x, y);
//                if (!(c.Red == 255 && c.Green == 255 && c.Blue == 255))
//                {
//                    foundRight = x;
//                    break;
//                }
//            }

//            if (foundLeft == -1 || foundRight == -1)
//            {
//                continue;
//            }

//            int rightDist = width - foundRight - 1;

//            if (foundSide)
//            {
//                left = Math.Min(left, foundLeft);
//                right = Math.Min(right, rightDist);
//            }
//            else
//            {
//                left = foundLeft;
//                right = rightDist;
//            }

//            foundSide = true;
//        }

//        if (!foundSide)
//        {
//            return false; // nothing to crop horizontally
//        }

//        int cropWidth = width - left - right;
//        int cropHeight = height - top - bottom;

//        if (cropWidth <= 0 || cropHeight <= 0)
//        {
//            return false;
//        }

//        // Create cropped bitmap
//        var cropped = new SKBitmap(cropWidth, cropHeight, bitmap.ColorType, bitmap.AlphaType);

//        using (var canvas = new SKCanvas(cropped))
//        {
//            var src = new SKRectI(left, top, left + cropWidth, top + cropHeight);
//            var dest = new SKRect(0, 0, cropWidth, cropHeight);
//            canvas.Clear(SKColors.Transparent);
//            canvas.DrawBitmap(bitmap, src, dest);
//            canvas.Flush();
//        }

//        using var image = SKImage.FromBitmap(cropped);
//        using var data = image.Encode(encodedFormat, 95);

//        using Stream output = await context.OpenWriteAsync().ConfigureAwait(false);

//        if (data != null)
//        {
//            data.SaveTo(output);
//            return true;
//        }

//        return false;
//    }
//}
