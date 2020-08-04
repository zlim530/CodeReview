using CoreDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.VIewComponents {
    public class MovieCountViewComponent:ViewComponent {
        private readonly IMovieService _movieService;

        public MovieCountViewComponent(IMovieService movieService) {
            _movieService = movieService;
        }

        public async Task<IViewComponentResult> InovkeAsync(int cinemaId) {
            var movies = await _movieService.GetByCinemaAsync(cinemaId);
            var count = movies.Count();

            return View(count);
        }
    }
}
