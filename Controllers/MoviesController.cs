using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMovies.Data;
using YourMovies.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace YourMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Movies.ToListAsync());
        }

        [Authorize]
        public async Task <IActionResult> Favourites()
        {
            var favourites = _db.Favourites.Include(f => f.Movie);
            return View(await favourites.ToListAsync());
        }

        [Authorize]
        [HttpGet]
        public async Task <IActionResult> AddFavourite(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = await _db.Movies.FindAsync(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFavourite(Movie movie)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favourite = new Favourite { UserId = userId, MovieId = movie.Id };

            _db.Favourites.Add(favourite);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
