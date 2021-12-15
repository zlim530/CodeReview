using Abp;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;

namespace YSR.MES.Movie.Movie.Dto
{
    public class DeleteMovieInfoInput : ICustomValidate
    {
        /// <summary>
        /// 主键 Id 集合
        /// </summary>
        public List<Guid> IdList { get; set; }

        /// <summary>
        /// 自定义检验
        /// </summary>
        /// <param name="context"></param>
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (IdList == null || IdList.Count == 0)
                throw new AbpException("主键Id不能为空！");
        }
    }
}
