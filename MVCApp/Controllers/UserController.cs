using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService=userService;
        }
        public async Task<IActionResult> ConfirmPurchase(string userid, string movieid)
        {
            DateTime curtime = DateTime.Now;
            PurchaseRequestModel purchaseRequest = new PurchaseRequestModel()
            {
                MovieId = Convert.ToInt32(movieid),
                UserId = Convert.ToInt32(userid),
                PurchaseDateTime = curtime,
                PurchaseNumber = Guid.NewGuid(),
            };
            await _userService.MakeMoviePurchase(purchaseRequest);
            return View();
        }
    }
}
