using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YSR.MES.Common.Algorithm;
using YSR.MES.Common.Attributes;
using YSR.MES.Common.CommonEnum;
using static Abp.SequentialGuidGenerator;

namespace YSR.MES.Movie
{
    /// <summary>
    /// 电影信息表
    /// </summary>
    [Table("MovieInfo")]
    [TableName("电影信息表", "MovieInfo", ContextTypeEnum.MovieDbContext)]
    public class MovieInfo : Entity<Guid>, IFullAudited
    {
        public MovieInfo()
        {
            Id = SequentialGuid.Create(SequentialGuidType.SequentialAsString);
        }

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

    #region SQL 创建表结构语句
    /*
    USE [Movie]
    GO
        SET ANSI_NULLS ON
        GO

    SET QUOTED_IDENTIFIER ON
    GO

    CREATE TABLE[dbo].[MovieInfo]
        (
        [Id][uniqueidentifier] NOT NULL,
        [Director] [nvarchar] (100) NOT NULL,
        [ProducingCountry] [nvarchar] (100) NOT NULL,
        [Language] [nvarchar] (100) NOT NULL,
        [Title] [nvarchar] (100) NOT NULL,
        [RelaseDate] [date] NOT NULL,
        [Genre] [varchar] (50) NOT NULL,
        [footage] [int] NOT NULL,
        [IsDeleted] [bit] NOT NULL,
        [CreatorUserId] [bigint] NULL,
	    [CreationTime] [datetime2] (7) NOT NULL,
        [LastModifierUserId] [bigint] NULL,
	    [LastModificationTime] [datetime2] (7) NULL,
	    [DeleterUserId] [bigint] NULL,
	    [DeletionTime] [datetime2] (7) NULL,
	    [DateTimeStamp] [timestamp] NULL,
    CONSTRAINT[PK_MovieInfo] PRIMARY KEY CLUSTERED
    (
        [Id] ASC
    )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
    ) ON[PRIMARY]
    GO

    EXEC sys.sp_addextendedproperty @name= N'MS_Description', @value= N'导演' , @level0type= N'SCHEMA', @level0name= N'dbo', @level1type= N'TABLE', @level1name= N'MovieInfo', @level2type= N'COLUMN', @level2name= N'Director'
    GO

    EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value= N'制片国家/地区' , @level0type= N'SCHEMA', @level0name= N'dbo', @level1type= N'TABLE', @level1name= N'MovieInfo', @level2type= N'COLUMN', @level2name= N'ProducingCountry'
    GO

    EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value= N'语言' , @level0type= N'SCHEMA', @level0name= N'dbo', @level1type= N'TABLE', @level1name= N'MovieInfo', @level2type= N'COLUMN', @level2name= N'Language'
    GO

    EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value= N'发行日期' , @level0type= N'SCHEMA', @level0name= N'dbo', @level1type= N'TABLE', @level1name= N'MovieInfo', @level2type= N'COLUMN', @level2name= N'RelaseDate'
    GO

    EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value= N'体裁/流派' , @level0type= N'SCHEMA', @level0name= N'dbo', @level1type= N'TABLE', @level1name= N'MovieInfo', @level2type= N'COLUMN', @level2name= N'Genre'
    GO

    EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value= N'片长' , @level0type= N'SCHEMA', @level0name= N'dbo', @level1type= N'TABLE', @level1name= N'MovieInfo', @level2type= N'COLUMN', @level2name= N'footage'
    GO
    */
    #endregion
}
