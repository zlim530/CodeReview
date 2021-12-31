using Abp.EntityFrameworkCore;
using Abp.Events.Bus.Entities;
using Abp.Extensions;
using Abp.Organizations;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using YSR.MES.Common.CommonModel.ExtendInterface;

namespace YSR.MES
{
    public class MESPlatFormBuilderBase<TSelf> : AbpDbContext where TSelf : MESPlatFormBuilderBase<TSelf>
    {
        #region 构造
        protected MESPlatFormBuilderBase(DbContextOptions<TSelf> options)
               : base(options)
        {

        }
        #endregion

        /// <summary>
        /// 是否禁止自动设置组织机构
        /// </summary>
        protected bool AutoSetDeptUnitId = true;

        /// <summary>
        /// 用户组织机构ID
        /// </summary>
        protected long UserOrgId => GetCurrentUsersOuIdOrNull();

        /// <summary>
        /// 用户组织机构ID权限集合
        /// </summary>
        protected List<long> UserOrgIdList => GetCurrentUsersOuListIDOrNull();

        /// <summary>
        /// 用户组织机构等级
        /// </summary>
        protected int UserOrgLevel => GetCurrentUsersOuLevelOrNull();

        /// <summary>
        /// 用户组织机构编码集合
        /// </summary>
        protected List<string> UserOrgCode => GetCurrentUsersOuCodeOrNull();

        public IPrincipalAccessor PrincipalAccessor { get; set; }

        #region 映射模型
        /// <summary>
        /// 映射模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //获取所有程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region 自动 填充实现 组织机构数据
        /// <summary>
        /// 重新添加是更新实体数据
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="userId"></param>
        /// <param name="changeReport"></param>
        protected override void ApplyAbpConceptsForAddedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            CheckAndSetMayHaveOrganizationUnitProperty(entry.Entity);
            base.ApplyAbpConceptsForAddedEntity(entry, userId, changeReport);
        }

        /// <summary>
        /// 设置 实体
        /// </summary>
        /// <param name="entityAsObj"></param>
        protected void CheckAndSetMayHaveOrganizationUnitProperty(object entityAsObj)
        {
            if (AutoSetDeptUnitId && entityAsObj is IMayHaveOrganizationUnit)
            {
                IMayHaveOrganizationUnit mayHaveOrganizationUnit = entityAsObj.As<IMayHaveOrganizationUnit>();
                mayHaveOrganizationUnit.OrganizationUnitId = GetCurrentUsersOuIdOrNull();
            }
        }
        #endregion


        #region 扩展审计自动过滤接口数据  获取组织机构ID、等级、Code
        protected virtual long GetCurrentUsersOuIdOrNull()
        {
            var userOuClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Application_OrganizationUnitId");

            if (string.IsNullOrEmpty(userOuClaim?.Value))
            {
                return 0;
            }
            return Convert.ToInt64(userOuClaim?.Value);
        }

        protected virtual List<long> GetCurrentUsersOuListIDOrNull()
        {
            var userOuClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Application_OrganizationUnitIdList");

            if (string.IsNullOrEmpty(userOuClaim?.Value))
            {
                return null;
            }
            return (Array.ConvertAll<string, long>(userOuClaim.Value.Split(","), long.Parse)).ToList();
        }

        protected virtual int GetCurrentUsersOuLevelOrNull()
        {
            var userOuClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Application_OrganizationLevel");

            if (string.IsNullOrEmpty(userOuClaim?.Value))
            {
                return 0;
            }
            return Convert.ToInt32(userOuClaim?.Value);
        }

        protected virtual List<string> GetCurrentUsersOuCodeOrNull()
        {
            var userOuClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "Application_OrganizationCode");

            if (string.IsNullOrEmpty(userOuClaim?.Value))
            {
                return null;
            }
            return userOuClaim.Value.Split(",").ToList();
        }

        protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
        {
            if (typeof(IDeptNo).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }
            return base.ShouldFilterEntity<TEntity>(entityType);
        }

        /// <summary>
        /// override-Expression
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            var expression = base.CreateFilterExpression<TEntity>();
            if (typeof(IDeptNo).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mayHaveDeptNoFilter = e => UserOrgCode.Contains(((IDeptNo)e).NewCode);
                expression = expression == null ? mayHaveDeptNoFilter != null ? mayHaveDeptNoFilter : null : CombineExpressions(expression, mayHaveDeptNoFilter);
            }
            if (typeof(IMayHaveOrganizationUnit).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> mayHaveOUFilter = e => UserOrgIdList.Contains((long)((IMayHaveOrganizationUnit)e).OrganizationUnitId);
                expression = expression == null ? mayHaveOUFilter != null ? mayHaveOUFilter : null : CombineExpressions(expression, mayHaveOUFilter);
            }
            return expression;
        }
        #endregion

        private static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(AbpDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);
    }
}
