using System;
using System.Collections.Generic;
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

        public async Task<Favorite> AddFavoriteMovie(int userid, int movieid)
        {
            var favorite = new Favorite() { MovieId=movieid,UserId=userid };
            await _dbContext.Favorites.AddAsync(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<Review> AddUserReviews(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Favorite> DropFavoriteMovie(int userid, int movieid)
        {
            var favorite = new Favorite() { MovieId = movieid, UserId = userid };
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<List<Favorite>> GetUserFavorites(int userid)
        {
            var user=await _dbContext.Users.Include(u => u.Favorites).FirstOrDefaultAsync();
            return new List<Favorite>(user.Favorites);
        }

        public async Task<List<Purchase>> GetUserPuchases(int userid)
        {
            var user = await _dbContext.Users.Include(u => u.Purchases).FirstOrDefaultAsync();
            return new List<Purchase>(user.Purchases);
        }

        public async Task<List<Review>> GetUserReviews(int userid)
        {
            var user = await _dbContext.Users.Include(u => u.Reviews).FirstOrDefaultAsync();
            return new List<Review>(user.Reviews);
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