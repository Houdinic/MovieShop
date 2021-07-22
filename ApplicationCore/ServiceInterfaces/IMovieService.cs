using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Entities;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieCardResponseModel>> GetMoviesInGenre(int id);
        Task<List<MovieCardResponseModel>> GetAll();
        Task<List<MovieCardResponseModel>> GetTopRatedMovies();
        Task<List<ReviewResponseModel>> GetMovieReviews(int id);
        Task<Movie> AddNewMovie(MovieCreateRequestModel model);
        Task<Movie> UpdateMovie(MovieCreateRequestModel model);
        Task<List<Purchase>> GetPurchase();
    }
}
