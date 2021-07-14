using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository

    {
        List<Movie> IMovieRepository.GetHighest30GrossingMovies()
        {
            throw new NotImplementedException();
        }
    }
}
