using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<List<GenreModel>> GetGenreModels()
        {
            List<Genre> genres=await _genreRepository.GetAllFromDb();
            var genreList = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genreList.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name=genre.Name

                });
                ;
            }
            return genreList; 
        }
    }
}
