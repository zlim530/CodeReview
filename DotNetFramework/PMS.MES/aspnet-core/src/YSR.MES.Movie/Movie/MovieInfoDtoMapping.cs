using AutoMapper;
using YSR.MES.Movie.Movie.Dto;

namespace YSR.MES.Movie.Movie
{
    public class MovieInfoDtoMapping : Profile
    {
        public MovieInfoDtoMapping()
        {
            CreateMap<CreateMovieInfoInput, MovieInfo>()
                .ForMember(s => s.Director,         map => map.MapFrom(d => d.d))
                .ForMember(s => s.Language,         map => map.MapFrom(d => d.lan))
                .ForMember(s => s.Title,            map => map.MapFrom(d => d.t))
                .ForMember(s => s.RelaseDate,       map => map.MapFrom(d => d.rD))
                .ForMember(s => s.Genre,            map => map.MapFrom(d => d.g))
                .ForMember(s => s.Footage,          map => map.MapFrom(d => d.f))
                .ForMember(s => s.ProducingCountry, map => map.MapFrom(d => d.pC));

            CreateMap<MovieInfo, PageMovieInfoOutput>()
                .ForMember(s => s.I,    map => map.MapFrom(d => d.Id))
                .ForMember(s => s.D,    map => map.MapFrom(d => d.Director))
                .ForMember(s => s.Lan,  map => map.MapFrom(d => d.Language))
                .ForMember(s => s.T,    map => map.MapFrom(d => d.Title))
                .ForMember(s => s.Rd,   map => map.MapFrom(d => d.RelaseDate))
                .ForMember(s => s.G,    map => map.MapFrom(d => d.Genre))
                .ForMember(s => s.Pc,   map => map.MapFrom(d => d.ProducingCountry))
                .ForMember(s => s.F,    map => map.MapFrom(d => d.Footage));
        }
    }
}
