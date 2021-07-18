using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;

namespace MovieShopMVC.Controllers
{
    public class CastController : Controller
    {   
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _castService.GetCastById(id));
        }
    }
}
