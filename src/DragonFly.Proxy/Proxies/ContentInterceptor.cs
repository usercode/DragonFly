// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Reflection;
using Castle.DynamicProxy;

namespace DragonFly.Proxy;

internal class ContentInterceptor : IInterceptor
{
    public ContentInterceptor(ContentItem contentItem)
    {
        ContentItem = contentItem;
    }

    private ContentItem ContentItem { get; }

    private static bool IsPropertyContentField(string propertyName, IInvocation invocation)
    {
        PropertyInfo? propertyInfo = invocation.TargetType.GetProperty(propertyName);

        if (propertyInfo == null)
        {
            throw new Exception($"Property not found: {propertyName}");
        }

        return propertyInfo.PropertyType.IsSubclassOf(typeof(ContentField));
    }

    public void Intercept(IInvocation invocation)
    {
        string methodName = invocation.Method.Name;

        if (methodName.StartsWith("get_") && invocation.Arguments.Length == 0)
        {
            string propertyName = methodName.Substring(4);

            if (ContentItem.TryGetField(propertyName, out ContentField? field))
            {
                if (IsPropertyContentField(propertyName, invocation) == false)
                {
                    if (field is ISingleValueField singleValueField)
                    {
                        invocation.ReturnValue = singleValueField.Value;

                        return;
                    }
                    else
                    {
                        throw new Exception($"The content field '{propertyName}' isn't a SingleValueField. ");
                    }
                }
                else
                {
                    invocation.ReturnValue = field;

                    return;
                }
            }
        }
        else if (methodName.StartsWith("set_") && invocation.Arguments.Length == 1)
        {
            string propertyName = methodName.Substring(4);
            object propertyValue = invocation.Arguments[0];

            if (IsPropertyContentField(propertyName, invocation) == false)
            {
                if (ContentItem.TryGetField(propertyName, out ContentField? contentField))
                {
                    if (contentField is ISingleValueField singleValueContentField)
                    {
                        singleValueContentField.Value = propertyValue;

                        return;
                    }
                    else
                    {
                        throw new Exception($"The content field '{propertyName}' isn't a SingleValueField. ");
                    }
                }
            }
            else
            {
                if (ContentItem.TrySetField(propertyName, (ContentField)propertyValue))
                {
                    return;
                }
            }
        }

        invocation.Proceed();
    }
}
