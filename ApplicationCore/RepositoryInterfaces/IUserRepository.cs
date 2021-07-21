using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task PurchaseMovie(Purchase purchase);
        Task<Favorite> AddFavoriteMovie(int userid, int movieid);
        Task<Favorite> DropFavoriteMovie(int userid, int movieid);
        Task<List<Purchase>> GetUserPuchases(int userid);
        Task<List<Review>> GetUserReviews(int userid);
        Task<List<Favorite>> GetUserFavorites(int userid);
        Task<Review> AddUserReviews(Review review);
    }
}