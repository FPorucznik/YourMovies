using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            return View(await _db.Movies.ToListAsync());
        }

        [Authorize]
        public IActionResult Favourites()
        {
            return View();
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
    }
}
