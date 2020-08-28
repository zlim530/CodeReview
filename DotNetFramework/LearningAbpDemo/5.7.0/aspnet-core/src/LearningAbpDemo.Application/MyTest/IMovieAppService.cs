using Abp.Application.Services;
using LearningAbpDemo.MyTest.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningAbpDemo.MyTest
{
    public interface IMovieAppService:IAsyncCrudAppService<MovieDTO,int,PagedMovieResultRequestDTO,CreateMovieDTO,MovieDTO>
    {
    }
}
