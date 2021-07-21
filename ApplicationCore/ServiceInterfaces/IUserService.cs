
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);

        Task<UserLoginResponseModel> Login(string email, string password);
        Task MakeMoviePurchase(PurchaseRequestModel purchaseRequest);
        Task<UserResponseModel> GetUserById(int id);
        Task<List<UserResponseModel>> GetAllUsers();
        Task AddFavorite(FavoriteRequestModel model);
        Task DropFavorite(FavoriteRequestModel model);
        Task<List<Purchase>> GetUserPuchases(int userid);
        Task<List<Review>> GetUserReviews(int userid);
        Task<List<Favorite>> GetUserFavorites(int userid);
        Task<Review> AddUserReviews(ReviewRequestModel model);
    }
}