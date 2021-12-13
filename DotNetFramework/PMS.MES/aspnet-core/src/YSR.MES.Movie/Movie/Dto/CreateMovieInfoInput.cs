using Abp;
using Abp.Runtime.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace YSR.MES.Movie.Movie.Dto
{
    public class CreateMovieInfoInput : ICustomValidate
    {
        /// <summary>
        /// 导演
        /// </summary>
        public string d { get; set; }

        /// <summary>
        /// 制片国家/地区
        /// </summary>
        public string pC { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string lan { get; set; }

        /// <summary>
        /// 片名
        /// </summary>
        public string t { get; set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? rD { get; set; }

        /// <summary>
        /// 体裁/流派
        /// </summary>
        public string g { get; set; }

        /// <summary>
        /// 片长
        /// </summary>
        [Required(ErrorMessage ="片长不能为空！")]
        public int f { get; set; }

        /// <summary>
        /// 自定义检验
        /// </summary>
        /// <param name="context"></param>
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrEmpty(d))
                throw new AbpException("导演不能为空！");
            if (string.IsNullOrEmpty(pC))
                throw new AbpException("制片国家/地区不能为空！");
            if (string.IsNullOrEmpty(lan))
                throw new AbpException("语言不能为空！");
            if (string.IsNullOrEmpty(t))
                throw new AbpException("片名不能为空！");
            if (string.IsNullOrEmpty(g))
                throw new AbpException("流派不能为空！");
            if (f <= 0)
                throw new AbpException("片长必须大于0！");
        }
    }
}
