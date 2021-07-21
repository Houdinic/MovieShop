using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetCastById")]
        public async Task<IActionResult> GetCastById(int id)
        {
            return Ok(await _castService.GetCastById(id));

        }
    }
}
