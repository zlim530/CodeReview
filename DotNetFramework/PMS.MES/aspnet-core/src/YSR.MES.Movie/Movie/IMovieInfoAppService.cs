using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSR.MES.Movie.Movie.Dto;

namespace YSR.MES.Movie.Movie
{
    public interface IMovieInfoAppService
    {
        /// <summary>
        /// 创建电影信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<Guid> CreateMovieInfoAsync(CreateMovieInfoInput input);
    }
}
