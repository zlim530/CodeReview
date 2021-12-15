using System;
using System.Threading.Tasks;
using YSR.MES.Common.CommonModel;
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

        /// <summary>
        /// 获取电影信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OutputPageInfo<PageMovieInfoOutput>> GetMovieInfosAsync(PageMovieInfoInput input);

        /// <summary>
        /// 删除电影信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DeleteMovieInfosAsync(DeleteMovieInfoInput input);
    }
}
