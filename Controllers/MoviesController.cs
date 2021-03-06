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
            var movies = _db.Movies.Include(m => m.Favourites);
            return View(await movies.ToListAsync());
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFavourite(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = await _db.Favourites.Include(f => f.Movie).SingleOrDefaultAsync(f => f.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFavourite(Favourite favourite)
        {
            _db.Favourites.Remove(favourite);
            await _db.SaveChangesAsync();

            return RedirectToAction("Favourites");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task <IActionResult> DeleteMovie(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var obj = await _db.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task <IActionResult> DeleteMovie(Movie movie)
        {
            var obj = await _db.Movies.Include(f => f.Favourites).SingleOrDefaultAsync(m => m.Id == movie.Id);
            _db.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task <IActionResult> AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _db.Movies.AddAsync(movie);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Movies");
            }
            return View(movie);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditMovie(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var obj = await _db.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _db.Movies.Update(movie);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Movies");
            }
            return View(movie);
        }
    }
}
