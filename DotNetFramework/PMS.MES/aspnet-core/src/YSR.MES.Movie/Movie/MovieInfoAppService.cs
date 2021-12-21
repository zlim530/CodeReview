using Abp;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSR.MES.Common.CommonModel;
using YSR.MES.Movie.Movie.Dto;

namespace YSR.MES.Movie.Movie
{
    /// <summary>
    /// 电影服务实现类
    /// </summary>
    public class MovieInfoAppService : MovieAppServiceBase, IMovieInfoAppService
    {
        private readonly IRepository<MovieInfo, Guid> _movieInfoRepository;
        private readonly IMapper _autoMapper;

        /// <summary>
        /// DI 构造函数依赖注入
        /// </summary>
        /// <param name="movieInfoReposiyory"></param>
        /// <param name="autoMapper"></param>
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
        [HttpDelete]
        public async Task<bool> DeleteMovieInfosAsync([FromBody] DeleteMovieInfoInput input)
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

        /// <summary>
        /// 编辑电影信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageMovieInfoOutput> EditMovieInfoAsync(EditMovieInfoInput input)
        {
            var obj = await _movieInfoRepository.FirstOrDefaultAsync(input.I);
            _ = obj ?? throw new AbpException($"未找到Id为:'{input.I}'的电影数据信息！");
            var editData = await _movieInfoRepository.FirstOrDefaultAsync(m => m.Director == input.D 
                                    && m.Language == input.Lan  && m.Title == input.T
                                    && m.RelaseDate == input.RD && m.Genre == input.G
                                    && m.Footage == input.F     && m.ProducingCountry == input.PC
                                    && m.Id != input.I);
            if (editData != null)
                throw new AbpException("已经存在此电影信息！");
            var updateData = _autoMapper.Map(input, obj);
            var data = await _movieInfoRepository.UpdateAsync(updateData);
            var result = _autoMapper.Map<PageMovieInfoOutput>(data);
            return result;
        }

    }
}
