using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Models;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;

        }

        public async Task<IActionResult> Index()
        {
            // var x = fnlsdfmlksd
            // var y = sdklfds;lfm

            var movies = await _movieService.GetTopRevenueMovies();
            // 1 ms, 20 ms, 10 seconds

            var myType = movies.GetType();

            // 3 ways to send the data from Controller/action to View
            // 1.*** Models (strongly typed models)
            // 2. ViewBag
            // 3. ViewData

            ViewBag.MoviesCount = movies.Count();

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
