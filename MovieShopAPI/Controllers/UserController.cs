using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> MakePurchase([FromBody]PurchaseRequestModel model)
        {
            await _userService.MakeMoviePurchase(model);
            return Ok();
        }
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite([FromBody]FavoriteRequestModel model)
        {
            await _userService.AddFavorite(model);
            return Ok();
        }
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> DropFavorite([FromBody] FavoriteRequestModel model)
        {
            await _userService.DropFavorite(model);
            return Ok();
        }

        [HttpGet]
        [Route("User/{id:int}/purchase")]
        public async Task<IActionResult> GetUserPurchases(int id)
        {
            return Ok(await _userService.GetUserPuchases(id));
        }
        [HttpGet]
        [Route("User/{id:int}/favorite")]
        public async Task<IActionResult> GetUserFavorites(int id)
        {
            return Ok(await _userService.GetUserFavorites(id));
        }
        [HttpGet]
        [Route("User/{id:int}/reviews", Name = "GetUserReviews")]
        public async Task<IActionResult> GetUserReviews(int id)
        {
            return Ok(await _userService.GetUserReviews(id));
        }
        [HttpPost]
        [Route("User/review")]
        public async Task<IActionResult> PostUserReviews([FromBody] ReviewRequestModel model)
        {
            return Ok(await _userService.AddUserReviews(model));
        }
        [HttpPut]
        [Route("User/review")]
        public async Task<IActionResult> UpdateUserReviews([FromBody] ReviewRequestModel model)
        {
            return Ok(await _userService.UpdateUserReviews(model));
        }
    }
}
