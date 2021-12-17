using Abp;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace YSR.MES.Movie.Movie.Dto
{
    public class EditMovieInfoInput : ICustomValidate
    {
        /// <summary>
        /// 主键 Id
        /// </summary>
        public Guid I { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string D { get; set; }

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
        public string RD { get; set; }

        /// <summary>
        /// 流派
        /// </summary>
        public string G { get; set; }

        /// <summary>
        /// 片长
        /// </summary>
        [Required(ErrorMessage ="片长不能为空！")]
        public int F { get; set; }

        /// <summary>
        /// 制片地区
        /// </summary>
        public string PC { get; set; }

        /// <summary>
        /// 自定义检验
        /// </summary>
        /// <param name="context"></param>
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrEmpty(D))
                throw new AbpException("导演不能为空！");
            if (string.IsNullOrEmpty(PC))
                throw new AbpException("制片国家/地区不能为空！");
            if (string.IsNullOrEmpty(Lan))
                throw new AbpException("语言不能为空！");
            if (string.IsNullOrEmpty(T))
                throw new AbpException("片名不能为空！");
            if (string.IsNullOrEmpty(G))
                throw new AbpException("流派不能为空！");
            if (F <= 0)
                throw new AbpException("片长必须大于0！");
        }
    }
}
