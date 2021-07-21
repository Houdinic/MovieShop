using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetAll()
        {
            var movies = await _movieRepository.ListAllAsync();
            var allmovies = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                allmovies.Add(new MovieCardResponseModel()
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                });
            }
            return allmovies;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var movieDetails = new MovieDetailsResponseModel() {

                Id = movie.Id,
                Title = movie.Title,
                Budget = movie.Budget.GetValueOrDefault(),
                PosterUrl = movie.PosterUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Revenue = movie.Revenue,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Price = movie.Price,
                Tagline = movie.Tagline,
                BackdropUrl = movie.BackdropUrl,

            };
            movieDetails.Casts = new List<CastResponseModel>();
            foreach (var cast in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Character=cast.Character,
                    Gender=cast.Cast.Gender,
                    TmdbUrl=cast.Cast.TmdbUrl,
                    ProfilePath=cast.Cast.ProfilePath

                }) ;
            }
            movieDetails.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name=genre.Name
                    
                });

            }

            return movieDetails;


        }

        public async Task<List<ReviewResponseModel>> GetMovieReviews(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var reviews = new List<ReviewResponseModel>();
            foreach (var review in movie.Reviews)
            {
                reviews.Add(new ReviewResponseModel()
                {
                    MovieId=review.MovieId,
                    UserId=review.UserId,
                    ReviewText=review.ReviewText,
                    Rating=review.Rating,
                });
            }
            return reviews;
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesInGenre(int id)
        {
            var movies = await _movieRepository.GetMovieByGenreId(id);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                   
                });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetBestRatedMovies();
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,

                });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighest30GrossingMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }

            return movieCards;
        }
        
    }
}
