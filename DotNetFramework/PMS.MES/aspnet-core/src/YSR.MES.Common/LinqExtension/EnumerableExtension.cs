using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YSR.MES.Common.CommonModel.LinqCommonModel;

namespace YSR.MES.Common.LinqExtension
{
    public static class EnumerableExtension
    {
        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="index">第几页</param>
        /// <param name="limit">一页多少条数据</param>
        /// <returns></returns>
        public static IEnumerable<TSource> SkipTakeEnumerable<TSource>(this IEnumerable<TSource> source, int index, int limit)
        {
            return source.Skip((index - 1) * limit).Take(limit);
        }
        #endregion

        #region 递归获取子节点的数据并赋值
        /// <summary>
        /// 递归获取子节点的数据并赋值
        /// </summary>
        /// <typeparam name="TEntity">输入的模型数据</typeparam>
        /// <typeparam name="OutEntity">要返回的dto</typeparam>
        /// <param name="entities">输入的集合</param>
        /// <param name="parentId">父级节点的id</param>
        /// <param name="func">映射的数据</param>
        /// <returns></returns>
        public static IEnumerable<OutEntity> GetChilds<TEntity, OutEntity>(IEnumerable<TEntity> entities, long parentId, Func<TEntity, OutEntity> func) where TEntity : Entity<long>, IParentId<long?> where OutEntity : IChildThis<OutEntity>
        {
            var topEntityModels = entities.Where(e => e.ParentId == parentId);

            List<OutEntity> outEntities = new List<OutEntity>();
            foreach (var topEntityModel in topEntityModels)
            {
                var outputDates = func(topEntityModel);
                outputDates.Children = GetChilds<TEntity, OutEntity>(entities, topEntityModel.Id, func);
                outEntities.Add(outputDates);
            }
            return outEntities;
        }
        #endregion

        #region 递归获取子节点的数据并赋值
        /// <summary>
        /// 递归获取子节点的数据并赋值
        /// </summary>
        /// <typeparam name="TEntity">输入的模型数据</typeparam>
        /// <param name="entities">输入的集合</param>
        /// <param name="parentId">父级节点的id</param>
        /// <param name="func">映射的数据</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> GetChildsByTEntity<TEntity>(
            IEnumerable<TEntity> entities,
            long parentId,
            Func<TEntity, TEntity> func)
            //where TEntity : Entity<long>, IParentId<long?> 
            where TEntity : Entity<long>, IChildThis<TEntity>, IParentId<long?>
        {
            var topEntityModels = entities.Where(e => e.ParentId == parentId);

            List<TEntity> outEntities = new List<TEntity>();
            foreach (var topEntityModel in topEntityModels)
            {
                var outputDates = func(topEntityModel);
                outputDates.Children = GetChildsByTEntity<TEntity>(entities, topEntityModel.Id, func);
                outEntities.Add(outputDates);
            }
            return outEntities;
        }
        #endregion

        #region 递归获取子节点的数据并赋值-int
        /// <summary>
        /// 递归获取子节点的数据并赋值-int
        /// </summary>
        /// <typeparam name="TEntity">输入的模型数据</typeparam>
        /// <typeparam name="OutEntity">要返回的dto</typeparam>
        /// <param name="entities">输入的集合</param>
        /// <param name="parentId">父级节点的id</param>
        /// <param name="func">映射的数据</param>
        /// <returns></returns>
        public static IEnumerable<OutEntity> GetChilds<TEntity, OutEntity>(IEnumerable<TEntity> entities, int parentId, Func<TEntity, OutEntity> func) where TEntity : Entity<int>, IParentId<int?> where OutEntity : IChildThis<OutEntity>
        {
            var topEntityModels = entities.Where(e => e.ParentId == parentId);

            List<OutEntity> outEntities = new List<OutEntity>();
            foreach (var topEntityModel in topEntityModels)
            {
                var outputDates = func(topEntityModel);
                outputDates.Children = GetChilds<TEntity, OutEntity>(entities, topEntityModel.Id, func);
                outEntities.Add(outputDates);
            }
            return outEntities;
        }
        #endregion

        #region 递归获取子节点的数据并赋值-long
        /// <summary>
        /// 递归获取子节点的数据并赋值-long
        /// </summary>
        /// <typeparam name="TEntity">输入的模型数据</typeparam>
        /// <typeparam name="OutEntity">要返回的dto</typeparam>
        /// <param name="entities">输入的集合</param>
        /// <param name="parentId">父级节点的id</param>
        /// <param name="func">映射的数据</param>
        /// <returns></returns>
        public static IEnumerable<OutEntity> GetToLongChilds<TEntity, OutEntity>(IEnumerable<TEntity> entities, long parentId, Func<TEntity, OutEntity> func) where TEntity : Entity<long>, IParentId<long> where OutEntity : IChildThis<OutEntity>
        {
            var topEntityModels = entities.Where(e => e.ParentId == parentId);

            List<OutEntity> outEntities = new List<OutEntity>();
            foreach (var topEntityModel in topEntityModels)
            {
                var outputDates = func(topEntityModel);
                outputDates.Children = GetToLongChilds<TEntity, OutEntity>(entities, topEntityModel.Id, func);
                outEntities.Add(outputDates);
            }
            return outEntities;
        }
        #endregion
    }
}
