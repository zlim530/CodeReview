using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YSR.MES.Common.LinqExtension
{
    public static class QueryableExtension
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="index">第几页</param>
        /// <param name="limit">一页多少条数据</param>
        /// <returns></returns>
        public static IQueryable<TSource> SkipTakeQueryble<TSource>(this IQueryable<TSource> source, int index, int limit)
        {
            return source.Skip((index - 1) * limit).Take(limit);
        }

        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName.Contains(propertyValue)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIfContains<TSource>(this IQueryable<TSource> source, string propertyName, string propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "p");
            MemberExpression member = Expression.PropertyOrField(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constant = Expression.Constant(propertyValue, typeof(string));
            return source.Where(Expression.Lambda<Func<TSource, bool>>(Expression.Call(member, method, constant), parameter));
        }
    }
}
