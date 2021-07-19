using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<CastResponseModel> GetCastById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castModel = new CastResponseModel()
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender=cast.Gender,
                TmdbUrl=cast.TmdbUrl,
                ProfilePath=cast.ProfilePath,
            };
            var movies = new List<MovieCardResponseModel>();
            castModel.MovieCards = movies;
            foreach (var movie in cast.MovieCasts)
            {
                movies.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    Budget = movie.Movie.Budget.GetValueOrDefault(),
                    PosterUrl = movie.Movie.PosterUrl,
                });
            }
            return castModel;
        }
    }
}
