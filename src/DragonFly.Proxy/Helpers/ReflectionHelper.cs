// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Linq.Expressions;
using System.Reflection;

namespace DragonFly.Proxy.Helpers;

static class ReflectionHelper
{
    public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
    {
        MemberExpression? member = propertyLambda.Body as MemberExpression;

        if (propertyLambda.Body is UnaryExpression unaryExpression)
        {
            member = unaryExpression.Operand as MemberExpression;
        }

        if (member == null)
        {
            throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
        }

        PropertyInfo? propInfo = member.Member as PropertyInfo;

        if (propInfo == null)
        {
            throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
        }

        return propInfo.Name;
    }
}
