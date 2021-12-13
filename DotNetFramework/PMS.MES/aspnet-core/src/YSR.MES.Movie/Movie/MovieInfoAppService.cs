using Abp.Domain.Repositories;
using AutoMapper;
using System;
using System.Threading.Tasks;
using YSR.MES.Movie.Movie.Dto;

namespace YSR.MES.Movie.Movie
{
    public class MovieInfoAppService : MovieAppServiceBase, IMovieInfoAppService
    {
        private readonly IRepository<MovieInfo, Guid> _movieInfoRepository;
        private readonly IMapper _autoMapper;

        public MovieInfoAppService(IRepository<MovieInfo, Guid> movieInfoReposiyory
            , IMapper autoMapper)
        {
            _movieInfoRepository = movieInfoReposiyory;
            _autoMapper = autoMapper;
        }

        /// <summary>
        /// 创建电影信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Guid> CreateMovieInfoAsync(CreateMovieInfoInput input)
        {
            var entity = _autoMapper.Map<MovieInfo>(input);
            var id = await _movieInfoRepository.InsertAndGetIdAsync(entity);
            return id;
        }
    }
}
