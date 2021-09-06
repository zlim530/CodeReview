using CoreDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreDemo.Services {
    public interface IMovieService {
        Task AddAsync(Movie model);

        Task<IEnumerable<Movie>> GetByCinemaAsync(int cinemaId);
    }
}
