using System;
using System.Linq.Expressions;

namespace ArkanoidGame.ViewModels.VMBase
{
    public static class SymbolHelpers
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpression = (propertyExpression.Body as MemberExpression);
            var propertyName = memberExpression.Member.Name;
            return propertyName;
        }
    }
}
