using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(await _movieService.GetAll());
        }
        [HttpGet]
        [Route("{id:int}",Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if (movie == null)
            {
                return NotFound($"No Movie Found for that {id}");
            }
            return Ok(movie);
        }
        // attribute based routing
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }

            return Ok(movies);

        }

        [HttpGet]
        [Route("genre/{id:int}")]
        public async Task<IActionResult> GetMoviesInGenreById(int id)
        {
            return Ok(await _movieService.GetMoviesInGenre(id));
        }

        [HttpGet]
        [Route("{id:int}/reviews",Name = "GetReivewById")]
        public async Task<IActionResult> GetReivewById(int id)
        {
            return Ok(await _movieService.GetMovieReviews(id));
        }
        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRated()
        {
            return Ok(await _movieService.GetTopRatedMovies());
        }


    }
}
