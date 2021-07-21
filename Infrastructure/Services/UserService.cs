using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task MakeMoviePurchase(PurchaseRequestModel purchaseRequest)
        {
            Purchase purchase = new Purchase() { 
            MovieId=purchaseRequest.MovieId,
            UserId=purchaseRequest.UserId,
            PurchaseDateTime=purchaseRequest.PurchaseDateTime,
            PurchaseNumber=purchaseRequest.PurchaseNumber,
            };
            await _userRepository.PurchaseMovie(purchase);
        }

        public async Task<UserLoginResponseModel> Login(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new NotFoundException("Email does not exists, please register first");
            }

            var hashedPssword = HashPassword(password, dbUser.Salt);

            if (hashedPssword == dbUser.HashedPassword)
            {
                // good, correct password

                var userLoginRespone = new UserLoginResponseModel
                {

                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    DateOfBirth = dbUser.DateOfBirth,
                    LastName = dbUser.LastName
                };

                return userLoginRespone;
            }

            return null;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Make sure email does not exists in database User table

            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);

            if (dbUser != null)
            {
                // we already have user with same email
                throw new ConflictException("Email arleady exists");
            }

            // create a unique salt

            var salt = CreateSalt();

            var hashedPassword = HashPassword(requestModel.Password, salt);

            // save to database

            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                HashedPassword = hashedPassword,
                DateOfBirth=requestModel.DateOfBirth,
            };

            // save to database by calling UserRepository Add method
            var createdUser = await _userRepository.AddAsync(user);

            var userResponse = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userResponse;
        }

        private string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string HashPassword(string password, string salt)
        {
            // Aarogon
            // Pbkdf2
            // BCrypt
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                    password: password,
                                                                    salt: Convert.FromBase64String(salt),
                                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                                    iterationCount: 10000,
                                                                    numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var userResponseModel = new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userResponseModel;
        }

        public async Task<List<UserResponseModel>> GetAllUsers()
        {
            var users= await _userRepository.ListAllAsync();
            var userModels = new List<UserResponseModel>();
            foreach (var user in users)
            {
                userModels.Add(new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth
                });
            }
            return userModels;
        }

        public async Task AddFavorite(FavoriteRequestModel model)
        {
            await _userRepository.AddFavoriteMovie(model.UserId, model.MovieId);
        }

        public async Task DropFavorite(FavoriteRequestModel model)
        {
            await _userRepository.DropFavoriteMovie(model.UserId, model.MovieId);
        }

        public async Task<List<Purchase>> GetUserPuchases(int userid)
        {
            return await _userRepository.GetUserPuchases(userid);
        }

        public async Task<List<Review>> GetUserReviews(int userid)
        {
            return await _userRepository.GetUserReviews(userid);
        }

        public async Task<List<Favorite>> GetUserFavorites(int userid)
        {
            return await _userRepository.GetUserFavorites(userid);
        }

        public async Task<Review> AddUserReviews(ReviewRequestModel model)
        {
            var review = new Review()
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                ReviewText = model.ReviewText,
                Rating = model.Rating,
            };
            return await _userRepository.AddUserReviews(review);
        }
    }
}