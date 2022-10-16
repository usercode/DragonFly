// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IDragonFlyModule
/// </summary>
public interface IDragonFlyModule
{
    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Description
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Author
    /// </summary>
    string Author { get; }

    /// <summary>
    /// Version
    /// </summary>
    Version Version { get; }


}
