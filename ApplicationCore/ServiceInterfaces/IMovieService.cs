using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

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
    }
}
