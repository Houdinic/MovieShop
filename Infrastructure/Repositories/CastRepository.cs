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
    public class CastRepository :  EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext):base(dbContext)
        {
        }
        public override async Task<Cast> GetByIdAsync(int id)
        {
            var cast = await _dbContext.Casts.Include(c => c.MovieCasts).ThenInclude(m => m.Movie).FirstOrDefaultAsync(c=>c.Id==id);
            if (cast ==null)
            {
                throw new Exception($"Didn't find Cast with id {id}");
            }
            return cast;
        }
    }
}
