using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YSR.MES.Common.Attributes;
using YSR.MES.Common.CommonEnum;

namespace YSR.MES
{
    [Table("SysPermission")]
    [TableName("系统权限表", "SysPermission", ContextTypeEnum.SYSDbContext)]
    public class Permission : Entity<long>, IFullAudited
    {
        /// <summary>
        /// 父节点 Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 权限模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 自定义排序
        /// </summary>
        public int? Sequence { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? DeletionTime { get; set; }

        public long? CreatorUserId { get; set; }

        public long? LastModifierUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? DeleterUserId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
