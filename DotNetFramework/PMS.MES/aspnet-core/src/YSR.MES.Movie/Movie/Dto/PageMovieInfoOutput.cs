using System;

namespace YSR.MES.Movie.Movie.Dto
{
    public class PageMovieInfoOutput
    {
        /// <summary>
        /// 主键 GUID
        /// </summary>
        public Guid I { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string D { get; set; }

        /// <summary>
        /// 制片国家/地区
        /// </summary>
        public string Pc { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Lan { get; set; }

        /// <summary>
        /// 片名
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public string Rd { get; set; }

        /// <summary>
        /// 流派
        /// </summary>
        public string G { get; set; }

        /// <summary>
        /// 片长
        /// </summary>
        public string F { get; set; }
    }
}
