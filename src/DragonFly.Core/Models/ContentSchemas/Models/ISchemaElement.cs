// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// It defines the fields of an <see cref="IContentElement"/>.<br/>
/// It's used by <see cref="ContentSchema"/> and <see cref="ArrayFieldOptions"/>.
/// </summary>
public interface ISchemaElement
{
    /// <summary>
    /// Fields
    /// </summary>
    SchemaFields Fields { get; }
}
