using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YSR.MES.Common.Attributes;
using YSR.MES.Common.CommonEnum;

namespace YSR.MES.Movie
{
    /// <summary>
    /// 电影信息表
    /// </summary>
    [Table("MovieInfo")]
    [TableName("电影信息表", "MovieInfo", ContextTypeEnum.MovieDbContext)]
    public class MovieInfo : Entity<Guid>, IFullAudited
    {
        /// <summary>
        /// 导演
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// 制片国家/地区
        /// </summary>
        public string ProducingCountry { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 片名
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? RelaseDate { get; set; }

        /// <summary>
        /// 体裁/流派
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// 片长
        /// </summary>
        public int Footage { get; set; }

        public long? CreatorUserId { get ; set ; }
        public DateTime CreationTime { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
