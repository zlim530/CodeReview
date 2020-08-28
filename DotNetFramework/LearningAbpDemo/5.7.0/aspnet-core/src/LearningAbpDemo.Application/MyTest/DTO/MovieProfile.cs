using AutoMapper;
using LearningAbpDemo.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningAbpDemo.MyTest.DTO
{
    public class MovieProfile:Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieDTO,Movie>();
            CreateMap<CreateMovieDTO, Movie>();
        }
    }
}
