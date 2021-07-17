using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>,IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext):base(dbContext)
        {
        }
        public async Task<List<Genre>>  GetAllFromDb() => await _dbContext.Genres.Select(g => g).ToListAsync();
    }
  
}
