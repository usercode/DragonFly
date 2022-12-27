// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public interface IAssetPreviewUrlService
{
    string CreateUrl(Asset asset, int width, int height);
}
