﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Castle.DynamicProxy;

namespace DragonFly.MongoDB.Proxies;

class AssetInterceptor : IInterceptor
{
    public AssetInterceptor(Guid assetId)
    {
        AssetId = assetId;

        _isLoaded = false;
        _firstLoad = false;
    }

    private Guid AssetId { get; }

    private bool _isLoaded;
    private bool _firstLoad;

    public void Intercept(IInvocation invocation)
    {
        Asset main = (Asset)invocation.InvocationTarget;

        if (_firstLoad == false)
        {
            main.Id = AssetId;

            _firstLoad = true;
        }

        if (invocation.Method.Name != $"get_{nameof(Asset.Id)}")
        {
            if (_isLoaded == false)
            {
                LoadData(main).GetAwaiter().GetResult();

                _isLoaded = true;
            }
        }

        invocation.Proceed();
    }

    private async Task LoadData(Asset main)
    {
        Asset? result = await MongoStorage.Default.GetAssetAsync(AssetId);

        main.Id = result.Id;
        main.CreatedAt = result.CreatedAt;
        main.ModifiedAt = result.ModifiedAt;
        main.Version = result.Version;
        main.Name = result.Name;
        main.Size = result.Size;
        main.MimeType = result.MimeType;
        main.Hash = result.Hash;
        main.Alt = result.Alt;
        main.Description = result.Description;
        main.Folder = result.Folder;
        main.Slug = result.Slug;
        main.PreviewUrl = result.PreviewUrl;
        main.Metaddata = result.Metaddata;
    }
}
