using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CastRepository : EfRepository<Cast>
    {
        public CastRepository(MovieShopDbContext dbContext):base(dbContext)
        {
        }

    }
}
