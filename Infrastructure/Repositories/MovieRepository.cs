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
            //var movies = await (from review in _dbContext.Reviews group review by review.MovieId into g select  }).ToListAsync();
            //var moviesGroup = await _dbContext.Reviews.Include(r=>r.Movie).GroupBy(r=>r.MovieId).OrderByDescending(r=>r.Average(m=>m.Rating));
            //var movies = await _dbContext.Reviews.GroupBy(m => m.MovieId).OrderByDescending(r => r.Average(r => r.Rating)).ToListAsync();
            //var res = new List<Movie>();
            //foreach (var movie in movies)
            //{
            //    res.Add(new Movie()
            //    {
            //        Id=movie.Key,

            //    });
            //}
            //moviesGroup.OrderBy()
            var topRatedMovies = await _dbContext.Reviews.Include(m => m.Movie)
                                                 .GroupBy(r => new
                                                 {
                                                     Id = r.MovieId,
                                                     r.Movie.PosterUrl,
                                                     r.Movie.Title,
                                                     r.Movie.ReleaseDate
                                                 })
                                                 .OrderByDescending(g => g.Average(m => m.Rating))
                                                 .Select(m => new Movie
                                                 {
                                                     Id = m.Key.Id,
                                                     PosterUrl = m.Key.PosterUrl,
                                                     Title = m.Key.Title,
                                                     ReleaseDate = m.Key.ReleaseDate,
                                                     Rating = m.Average(x => x.Rating)
                                                 })
                                                 .Take(50)
                                                 .ToListAsync();

            return topRatedMovies;
        }

        public async Task<List<Purchase>> GetPurchases()
        {
            return await _dbContext.Purchases.ToListAsync();
        }
    }
}
