using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.DataAccess
{
    public static class Lambda
    {
        public static Expression<Func<TSource, TProperty>> ForProperty<TSource, TProperty>(string propertyName)
        {
            var parameterExpression = Expression.Parameter(typeof(TSource));
            var memberExpression = Expression.Property(parameterExpression, propertyName);
            var expressionBody = Expression.Convert(memberExpression, typeof(TProperty));
            return Expression.Lambda<Func<TSource, TProperty>>(expressionBody, parameterExpression);
        }
    }

    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            var lambda = Lambda.ForProperty<TSource, object>(propertyName);
            return source.OrderBy(lambda);
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string propertyName)
        {
            var lambda = Lambda.ForProperty<TSource, object>(propertyName);
            return source.OrderByDescending(lambda);
        }
    }
}
