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
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public AdminController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> AddNewMovie([FromBody] MovieCreateRequestModel model)
        {
            return Ok(await _movieService.AddNewMovie(model));

        }
        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateNewMovie([FromBody] MovieCreateRequestModel model)
        {
            return Ok(await _movieService.UpdateMovie(model));

        }
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetPuchasesData()
        {
            return Ok(await _movieService.GetPurchase());
        }
    }
}
