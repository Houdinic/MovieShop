using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext) { }

        public async Task<List<Movie>> GetHighest30GrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

            var movieRating = await _dbContext.Reviews.Where(m => m.MovieId == id).DefaultIfEmpty()
              .AverageAsync(r => r == null ? 0 : r.Rating);

            if (movieRating > 0)
            {
                movie.Rating = movieRating;
            }

            return movie;
        }

        public async Task<List<Movie>> GetMovieByGenreId(int id)
        {
            var res = await _dbContext.Genres.Where(g => g.Id == id).Select(g => g.Movies).SingleAsync();
            return new List<Movie>(res);
        }

        public async Task<List<Movie>> GetBestRatedMovies()
        {
            throw new NotImplementedException();
            //var movies = await (from review in _dbContext.Reviews group review by review.MovieId into g select  }).ToListAsync();
            //var moviesGroup = await _dbContext.Reviews.GroupBy(m => m.MovieId,m=>new { Rating = m.Rating, avg = 0 }).ToListAsync();
            //moviesGroup.OrderBy()
    }
    }
}
