// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Castle.DynamicProxy;

namespace DragonFly.MongoDB.Proxies;

class AssetFolderInterceptor : IInterceptor
{
    public AssetFolderInterceptor(Guid assetFolderId)
    {
        AssetFolderId = assetFolderId;

        _isLoaded = false;
        _firstLoad = false;
    }

    private Guid AssetFolderId { get; }

    private bool _isLoaded;
    private bool _firstLoad;

    public void Intercept(IInvocation invocation)
    {
        AssetFolder main = (AssetFolder)invocation.InvocationTarget;

        if (_firstLoad == false)
        {
            main.Id = AssetFolderId;

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

    private async Task LoadData(AssetFolder main)
    {
        var result = await MongoStorage.Default.GetAssetFolderAsync(AssetFolderId);

        main.Id = result.Id;
        main.CreatedAt = result.CreatedAt;
        main.ModifiedAt = result.ModifiedAt;
        main.Version = result.Version;
        main.Name = result.Name;
        main.Parent = result.Parent;
    }
}
