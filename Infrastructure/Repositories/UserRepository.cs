using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task PurchaseMovie(Purchase purchase)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == purchase.MovieId);
            purchase.TotalPrice = (decimal)movie.Price;
            await _dbContext.Purchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();
        }
    }
}