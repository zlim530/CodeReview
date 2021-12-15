using Abp;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSR.MES.Common.CommonModel;
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

        /// <summary>
        /// 获取电影信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OutputPageInfo<PageMovieInfoOutput>> GetMovieInfosAsync(PageMovieInfoInput input)
        {
            var skipCount = (input.Page - 1) * input.Limit;

            var query = _movieInfoRepository.GetAll()
                                        .WhereIf(!string.IsNullOrEmpty(input.t), m => m.Title.Contains(input.t));

            var list = await query.Skip(skipCount).Take(input.Limit).ToListAsync();
            var count = await query.CountAsync();
            var result = _autoMapper.Map<List<PageMovieInfoOutput>>(list);

            return new OutputPageInfo<PageMovieInfoOutput>(count, result);
        }

        /// <summary>
        /// 删除电影信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMovieInfosAsync(DeleteMovieInfoInput input)
        {
            var deletedIds = await _movieInfoRepository.GetAll()
                                    .Where(m => input.IdList.Contains(m.Id))
                                    .Select(m => m.Id)
                                    .ToListAsync();

            var checkedData = input.IdList.Where(i => !deletedIds.Contains(i));
            if (checkedData.Any())
                throw new AbpException($"删除失败！Id为'{string.Join(",",checkedData)}'的数据不存在！");
            await _movieInfoRepository.GetAll().Where(m => input.IdList.Contains(m.Id))
                                    .BatchUpdateAsync(_ => new MovieInfo
                                                            {
                                                                IsDeleted = true
                                                            });

            return await Task.FromResult(true);
        }
    }
}
