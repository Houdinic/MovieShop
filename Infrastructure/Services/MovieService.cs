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
        private readonly IMovieRepository _movieRepositoy;
        public MovieService(IMovieRepository movieRepositoy)
        {
            _movieRepositoy =  movieRepositoy;
        }

        public Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            throw new NotImplementedException();
        }
        //public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        //{
        //    var movies = await _movieRepositoy.GetHighest30GrossingMovies();
        //    var movieCards = new List<MovieCardResponseModel>();

        //}

    }
}
