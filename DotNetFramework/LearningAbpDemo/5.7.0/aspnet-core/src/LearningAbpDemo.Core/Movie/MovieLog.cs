using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningAbpDemo.Movie
{
    [Table("MovieLog")]
    [TableName("电影记录表","MovieLog",Common.EnumCommon.ContextTypeEnum.MovieDbConext)]
    public class MovieLog : Entity<int>, IFullAudited
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        
        /// <summary>
        /// 上映时间
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime RelaseDate { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Genre  { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }


        // IFullAudited 接口中的方法
        public bool IsDeleted { get ; set ; }
        public long? CreatorUserId { get ; set; }
        public DateTime CreationTime { get ; set; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get; set ; }
        public long? DeleterUserId { get ; set; }
        public DateTime? DeletionTime { get ; set; }
    }
}
