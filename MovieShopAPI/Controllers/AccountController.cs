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
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            var createdUser = await _userService.RegisterUser(model);

            // send the URL for newly created user also
            // 5000

            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser);

            // 201
        }
        //[HttpPost]
        //public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestModel model)
        //{
        //    var loginUser = await _userService.Login(model.Email, model.Password);

        //    return CreatedAtRoute("GetUser", new { id = loginUser.Id }, loginUser);

        //    // 201
        //}

        [HttpGet]
        [Route("{id:int}", Name = "GetUser")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound($"User does not exists for {id}");
            }

            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

    }
}
