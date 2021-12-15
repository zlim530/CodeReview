using Abp;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Runtime.Session;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace YSR.MES.Common.LinqExtension
{
    public static class RepositoryExtension
    {
        private readonly static IAbpSession _abpSession;
        private static string _strCreateUserId;
        private static string _datetimeCreateDateTimes;
        private static string _strUpdateUserId;
        private static string _datetimeUpdateDateTimes;
        private static string _strOrganizationUnitId;
        public static IPrincipalAccessor PrincipalAccessor { get; set; }


        static RepositoryExtension()
        {
            _abpSession = IocManager.Instance.Resolve<IAbpSession>();
            _strCreateUserId = "CreatorUserId";
            _datetimeCreateDateTimes = "CreationTime";
            _strUpdateUserId = "LastModifierUserId";
            _datetimeUpdateDateTimes = "LastModificationTime";
            _strOrganizationUnitId = "OrganizationUnitId";
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public async static Task NewBatchDeleteAsync<TEntity, TPrimaryKey>([NotNull] this IRepository<TEntity, TPrimaryKey> repository, [NotNull] Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<TPrimaryKey>, IDeletionAudited, new()
        {
            Check.NotNull(repository, nameof(repository));
            Check.NotNull(predicate, nameof(predicate));
            await repository.GetAll().Where(predicate).BatchUpdateAsync(e => new TEntity() { IsDeleted = true, DeletionTime = DateTime.Now, DeleterUserId = _abpSession.UserId });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public async static Task NewBatchSoftDeleteAsync<TEntity, TPrimaryKey>([NotNull] this IRepository<TEntity, TPrimaryKey> repository, [NotNull] Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<TPrimaryKey>, ISoftDelete, new()
        {
            Check.NotNull(repository, nameof(repository));
            Check.NotNull(predicate, nameof(predicate));

            await repository.GetAll().Where(predicate).BatchUpdateAsync(e => new TEntity() { IsDeleted = true });
        }

        /// <summary>
        /// 删除数据并返回受影响行数
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async static Task<int> NewBatchDeleteWithCountAsync<TEntity, TPrimaryKey>([NotNull] this IRepository<TEntity, TPrimaryKey> repository, [NotNull] Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<TPrimaryKey>, IDeletionAudited, new()
        {
            Check.NotNull(repository, nameof(repository));
            Check.NotNull(predicate, nameof(predicate));

            var count = await repository.GetAll().Where(predicate).BatchUpdateAsync(e => new TEntity() { IsDeleted = true, DeletionTime = DateTime.Now, DeleterUserId = _abpSession.UserId });
            return count;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async static Task BulkInsertAsync<TEntity, TPrimaryKey>([NotNull] this IRepository<TEntity, TPrimaryKey> repository, List<TEntity> entities) where TEntity : Entity<TPrimaryKey>, new()
        {
            #region 检查对象不为空
            if (entities.Any())
            {
                #region 遍历对象
                foreach (var item in entities)
                {
                    var itemType = item.GetType();
                    var objcuser = itemType.GetProperty(_strCreateUserId);
                    if (objcuser != null)
                        objcuser.SetValue(item, _abpSession.UserId, null);
                    var objctime = itemType.GetProperty(_datetimeCreateDateTimes);
                    if (objctime != null)
                        objctime.SetValue(item, DateTime.Now, null);
                }
                #endregion
            }
            #endregion
            await repository.GetDbContext().AddRangeAsync(entities);
            await repository.GetDbContext().SaveChangesAsync();
        }
        /// <summary>
        /// 批量插入 不设置插入时间
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async static Task BulkInsertWithTimeAsync<TEntity, TPrimaryKey>([NotNull] this IRepository<TEntity, TPrimaryKey> repository, List<TEntity> entities) where TEntity : Entity<TPrimaryKey>, new()
        {
            #region 检查对象不为空
            if (entities.Any())
            {
                #region 遍历对象
                foreach (var item in entities)
                {
                    var itemType = item.GetType();
                    var objcuser = itemType.GetProperty(_strCreateUserId);
                    if (objcuser != null)
                        objcuser.SetValue(item, _abpSession.UserId, null);
                }
                #endregion
            }
            #endregion
            await repository.GetDbContext().AddRangeAsync(entities);
            await repository.GetDbContext().SaveChangesAsync();
        }
        /// <summary>
        /// 批量插入-插入自定义组织机构属性
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entities"></param>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public async static Task BulkInsertForOrgIdAsync<TEntity, TPrimaryKey>([NotNull] this IRepository<TEntity, TPrimaryKey> repository, List<TEntity> entities, long? orgid = null) where TEntity : Entity<TPrimaryKey>, new()
        {

            #region 检查对象不为空
            if (entities.Any())
            {
                #region 遍历对象
                foreach (var item in entities)
                {
                    var itemType = item.GetType();
                    var objcuser = itemType.GetProperty(_strCreateUserId);
                    if (objcuser != null)
                        objcuser.SetValue(item, _abpSession.UserId, null);
                    var objctime = itemType.GetProperty(_datetimeCreateDateTimes);
                    if (objctime != null)
                        objctime.SetValue(item, DateTime.Now, null);
                    var objorg = itemType.GetProperty(_strOrganizationUnitId);
                    if (objorg != null && orgid != null)
                        objorg.SetValue(item, orgid, null);
                }
                #endregion
            }
            #endregion
            await repository.GetDbContext().AddRangeAsync(entities);
            await repository.GetDbContext().SaveChangesAsync();
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async static Task BulkUpdateAsync<TEntity, TPrimaryKey>([NotNull] this IRepository<TEntity, TPrimaryKey> repository, List<TEntity> entities) where TEntity : Entity<TPrimaryKey>, new()
        {
            #region 检查对象不为空
            if (entities.Any())
            {
                #region 遍历对象
                foreach (var item in entities)
                {
                    var itemType = item.GetType();
                    var objupuser = itemType.GetProperty(_strUpdateUserId);
                    if (objupuser != null)
                        objupuser.SetValue(item, _abpSession.UserId, null);
                    var objuptime = itemType.GetProperty(_datetimeUpdateDateTimes);
                    if (objuptime != null)
                        objuptime.SetValue(item, DateTime.Now, null);
                }
                #endregion
            }
            #endregion
            repository.GetDbContext().ChangeTracker.Entries().Any(x => entities.Any(y => x.Entity == y));
            await repository.GetDbContext().SaveChangesAsync();
        }
    }
}
