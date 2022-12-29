// Copyright (c) usercode
// https://github.com/usercode/AspNetCore.Decorator
// MIT License

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AspNetCore.Decorator;

class DecoratedType : Type
{
    public DecoratedType(Type type) => ProxyType = type;
    private Type ProxyType { get; }
    public override bool ContainsGenericParameters => ProxyType.ContainsGenericParameters;
    public override Type[] GenericTypeArguments => ProxyType.GenericTypeArguments;
    public override bool IsCollectible => ProxyType.IsCollectible;
    public override bool IsSerializable => ProxyType.IsSerializable;
    public override bool HasSameMetadataDefinitionAs(MemberInfo other) => ProxyType.HasSameMetadataDefinitionAs(other);
    public override bool IsEnumDefined(object value) => ProxyType.IsEnumDefined(value);
    public override bool IsAssignableFrom([NotNullWhen(true)] Type? c) => ProxyType.IsAssignableFrom(c);
    public override string[] GetEnumNames() => ProxyType.GetEnumNames();
    public override bool IsSubclassOf(Type c) => ProxyType.IsSubclassOf(c);
    public override Type[] FindInterfaces(TypeFilter filter, object? filterCriteria) => ProxyType.FindInterfaces(filter, filterCriteria);
    public override MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter? filter, object? filterCriteria) => ProxyType.FindMembers(memberType, bindingAttr, filter, filterCriteria);
    public override string? GetEnumName(object value) => ProxyType.GetEnumName(value);
    public override bool Equals(Type? o) => ReferenceEquals(this, o);
    public override bool Equals(object? o) => ReferenceEquals(this, o);
    public override int GetHashCode() => ProxyType.GetHashCode();
    public override string? Namespace => ProxyType.Namespace;
    public override string? AssemblyQualifiedName => ProxyType.AssemblyQualifiedName;
    public override string? FullName => ProxyType.FullName;
    public override Assembly Assembly => ProxyType.Assembly;
    public override Module Module => ProxyType.Module;
    public override Type? DeclaringType => ProxyType.DeclaringType;
    public override MethodBase? DeclaringMethod => ProxyType.DeclaringMethod;
    public override Type? ReflectedType => ProxyType.ReflectedType;
    public override Type UnderlyingSystemType => ProxyType.UnderlyingSystemType;
    public override bool IsTypeDefinition => ProxyType.IsTypeDefinition;
    protected override bool IsArrayImpl() => ProxyType.HasElementType;
    protected override bool IsByRefImpl() => ProxyType.IsByRef;
    protected override bool IsPointerImpl() => ProxyType.IsPointer;
    public override bool IsConstructedGenericType => ProxyType.IsConstructedGenericType;
    public override bool IsGenericParameter => ProxyType.IsGenericParameter;
    public override bool IsGenericTypeParameter => ProxyType.IsGenericTypeParameter;
    public override bool IsGenericMethodParameter => ProxyType.IsGenericMethodParameter;
    public override bool IsGenericType => ProxyType.IsGenericType;
    public override bool IsGenericTypeDefinition => ProxyType.IsGenericTypeDefinition;
    public override bool IsSZArray => ProxyType.IsSZArray;
    public override bool IsVariableBoundArray => ProxyType.IsVariableBoundArray;
    public override bool IsByRefLike => ProxyType.IsByRefLike;
    protected override bool HasElementTypeImpl() => ProxyType.HasElementType;
    public override Type? GetElementType() => ProxyType.GetElementType();
    public override int GetArrayRank() => ProxyType.GetArrayRank();
    public override Type GetGenericTypeDefinition() => ProxyType.GetGenericTypeDefinition();
    public override Type[] GetGenericArguments() => ProxyType.GetGenericArguments();
    public override int GenericParameterPosition => ProxyType.GenericParameterPosition;
    public override GenericParameterAttributes GenericParameterAttributes => ProxyType.GenericParameterAttributes;
    public override Type[] GetGenericParameterConstraints() => ProxyType.GetGenericParameterConstraints();
    protected override TypeAttributes GetAttributeFlagsImpl() => ProxyType.Attributes;
    protected override bool IsCOMObjectImpl() => ProxyType.IsCOMObject;
    protected override bool IsContextfulImpl() => ProxyType.IsContextful;
    public override bool IsEnum => ProxyType.IsEnum;
    protected override bool IsMarshalByRefImpl() => ProxyType.IsMarshalByRef;
    protected override bool IsPrimitiveImpl() => ProxyType.IsPrimitive;
    protected override bool IsValueTypeImpl() => ProxyType.IsValueType;

    public override bool IsSignatureType => ProxyType.IsSignatureType;

    public override bool IsSecurityCritical => ProxyType.IsSecurityCritical;
    public override bool IsSecuritySafeCritical => ProxyType.IsSecuritySafeCritical;
    public override bool IsSecurityTransparent => ProxyType.IsSecurityTransparent;
    public override StructLayoutAttribute? StructLayoutAttribute => ProxyType.StructLayoutAttribute;
    protected override ConstructorInfo? GetConstructorImpl(BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[] types, ParameterModifier[]? modifiers)
        => ProxyType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => ProxyType.GetConstructors(bindingAttr);
    public override EventInfo? GetEvent(string name, BindingFlags bindingAttr) => ProxyType.GetEvent(name, bindingAttr);
    public override EventInfo[] GetEvents() => ProxyType.GetEvents();
    public override EventInfo[] GetEvents(BindingFlags bindingAttr) => ProxyType.GetEvents(bindingAttr);
    public override FieldInfo? GetField(string name, BindingFlags bindingAttr) => ProxyType.GetField(name, bindingAttr);
    public override FieldInfo[] GetFields(BindingFlags bindingAttr) => ProxyType.GetFields(bindingAttr);
    public override MemberInfo[] GetMember(string name, BindingFlags bindingAttr) => ProxyType.GetMember(name, bindingAttr);
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr) => ProxyType.GetMember(name, type, bindingAttr);

    public override MemberInfo GetMemberWithSameMetadataDefinitionAs(MemberInfo member) => ProxyType.GetMemberWithSameMetadataDefinitionAs(member);

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => ProxyType.GetMembers(bindingAttr);
    protected override MethodInfo? GetMethodImpl(string name, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[]? types, ParameterModifier[]? modifiers)
        => ProxyType.GetMethod(name, bindingAttr, binder, callConvention, types!, modifiers);
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr) => ProxyType.GetMethods(bindingAttr);
    public override Type? GetNestedType(string name, BindingFlags bindingAttr) => ProxyType.GetNestedType(name, bindingAttr);
    public override Type[] GetNestedTypes(BindingFlags bindingAttr) => ProxyType.GetNestedTypes(bindingAttr);
    protected override PropertyInfo? GetPropertyImpl(string name, BindingFlags bindingAttr, Binder? binder, Type? returnType, Type[]? types, ParameterModifier[]? modifiers)
        => ProxyType.GetProperty(name, bindingAttr, binder, returnType, types!, modifiers);
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr) => ProxyType.GetProperties(bindingAttr);
    public override MemberInfo[] GetDefaultMembers() => ProxyType.GetDefaultMembers();
    public override RuntimeTypeHandle TypeHandle => ProxyType.TypeHandle;
    protected override TypeCode GetTypeCodeImpl() => GetTypeCode(ProxyType);
    public override Guid GUID => ProxyType.GUID;
    public override Type? BaseType => ProxyType.BaseType;
    public override object? InvokeMember(string name, BindingFlags invokeAttr, Binder? binder, object? target, object?[]? args, ParameterModifier[]? modifiers, CultureInfo? culture, string[]? namedParameters) =>
        ProxyType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    public override Type? GetInterface(string name, bool ignoreCase) => ProxyType.GetInterface(name, ignoreCase);
    public override Type[] GetInterfaces() => ProxyType.GetInterfaces();
    public override InterfaceMapping GetInterfaceMap(Type interfaceType) => ProxyType.GetInterfaceMap(interfaceType);
    public override bool IsInstanceOfType(object? o) => ProxyType.IsInstanceOfType(o);
    public override bool IsEquivalentTo(Type? other) => ProxyType.IsEquivalentTo(other);
    public override Type GetEnumUnderlyingType() => ProxyType.GetEnumUnderlyingType();
    public override Array GetEnumValues() => ProxyType.GetEnumValues();
    public override Type MakeArrayType() => ProxyType.MakeArrayType();
    public override Type MakeArrayType(int rank) => ProxyType.MakeArrayType(rank);
    public override Type MakeByRefType() => ProxyType.MakeByRefType();
    public override Type MakeGenericType(params Type[] typeArguments) => ProxyType.MakeGenericType(typeArguments);
    public override Type MakePointerType() => ProxyType.MakePointerType();
    public override string ToString() => "Type: " + Name;
    public override MemberTypes MemberType => ProxyType.MemberType;
    public override string Name => $"{ProxyType.Name}__Decorated";
    public override IEnumerable<CustomAttributeData> CustomAttributes => ProxyType.CustomAttributes;
    public override int MetadataToken => ProxyType.MetadataToken;
    public override object[] GetCustomAttributes(bool inherit) => ProxyType.GetCustomAttributes(inherit);
    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => ProxyType.GetCustomAttributes(attributeType, inherit);
    public override bool IsDefined(Type attributeType, bool inherit) => ProxyType.IsDefined(attributeType, inherit);
    public override IList<CustomAttributeData> GetCustomAttributesData() => ProxyType.GetCustomAttributesData();
}
