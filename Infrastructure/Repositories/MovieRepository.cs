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
        public MovieRepository(MovieShopDbContext _Dbcontext)
        {

        }
        public async Task<List<Movie>> GetHighest30GrossingMovies()
        {
            var TopMovies = await _DbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return TopMovies;
        }
    }
}
