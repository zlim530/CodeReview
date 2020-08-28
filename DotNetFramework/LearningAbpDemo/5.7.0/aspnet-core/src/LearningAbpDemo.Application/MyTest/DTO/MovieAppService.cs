using Abp.Application.Services;
using Abp.Domain.Repositories;
using LearningAbpDemo.Test;
using System.Linq;

namespace LearningAbpDemo.MyTest.DTO
{
    public class MovieAppService : AsyncCrudAppService<Movie, MovieDTO, int, PagedMovieResultRequestDTO, CreateMovieDTO, MovieDTO>, IMovieAppService
    {
        public MovieAppService(IRepository<Movie, int> repository) : base(repository)
        {
        }


        /// <summary>
        /// 条件guolv
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Movie> CreateFilteredQuery(PagedMovieResultRequestDTO input)
        {
            return base.CreateFilteredQuery(input);
        }
    }
}
