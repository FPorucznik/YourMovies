using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMovies.Data;
using YourMovies.Models;

namespace YourMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Movie> moviesList = _db.Movies;
            return View(moviesList);
        }

        [Authorize]
        public IActionResult Favourites()
        {
            return View();
        }
    }
}
