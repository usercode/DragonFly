// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;

namespace DragonFly.Client;

public interface IContentAction<T>
{
    string Name { get; }

    bool CanUse(T contentItemDetail);

    Task Execute(T contentItemBase);
}
