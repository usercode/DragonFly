// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Assets;

public interface IAssetActionContext
{
    Task<Stream> OpenReadAsync();

    Task<Stream> OpenWriteAsync();
}
