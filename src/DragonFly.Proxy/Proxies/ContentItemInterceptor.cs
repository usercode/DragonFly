// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Reflection;
using Castle.DynamicProxy;

namespace DragonFly.Proxy;

internal class ContentItemInterceptor : IInterceptor
{
    public ContentItemInterceptor(ContentItem contentItem)
    {
        ContentItem = contentItem;
    }

    private ContentItem ContentItem { get; }

    public void Intercept(IInvocation invocation)
    {
        if (invocation.Method.Name.StartsWith("get_"))
        {
            string propertyName = invocation.Method.Name.Substring(4);

            if (ContentItem.TryGetField(propertyName, out IContentField? field))
            {
                if (field is ISingleValueField singleValueField)
                {
                    PropertyInfo? propertyInfo = invocation.TargetType.GetProperty(propertyName);

                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property not found: {propertyName}");
                    }

                    if (propertyInfo.PropertyType.IsPrimitive
                        || propertyInfo.PropertyType == typeof(string)
                        || (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        invocation.ReturnValue = singleValueField.Value;

                        return;
                    }
                }

                invocation.ReturnValue = field;

                return;
            }
        }
        else if (invocation.Method.Name.StartsWith("set_"))
        {
            string propertyName = invocation.Method.Name.Substring(4);

            if (invocation.Arguments.Length > 0)
            {
                object value = invocation.Arguments[0];
                Type valueType = value.GetType();

                if (valueType.IsPrimitive 
                    || valueType == typeof(string) 
                    || (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    if (ContentItem.TryGetField(propertyName, out IContentField? contentField))
                    {
                        if (contentField is ISingleValueField singleValueContentField)
                        {
                            singleValueContentField.Value = value;

                            return;
                        }
                    }
                }
            }

            if (ContentItem.TrySetField(propertyName, (IContentField)invocation.Arguments[0]))
            {
                return;
            }
        }

        invocation.Proceed();
    }
}
