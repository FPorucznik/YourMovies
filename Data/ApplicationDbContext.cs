using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YourMovies.Models;

namespace YourMovies.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>().HasData(new Movie { Id=1, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/5/5a/Pirates_AWE_Poster.jpg", Title = "Pirates of the Caribbean: At world's end", ReleaseYear = 2007, Director = "Gore Verbinski", Genre = "Adventure" });
            builder.Entity<Movie>().HasData(new Movie { Id=2, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/b9/Shrek_2_poster.jpg", Title = "Shrek 2", ReleaseYear = 2004, Director = "Andrew Adamson", Genre = "Animated/Adventure/Comedy" });
            builder.Entity<Movie>().HasData(new Movie { Id=3, ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/b9/Shrek_2_poster.jpg", Title = "Titanic", ReleaseYear = 1997, Director = "James Cameron", Genre = "Romance/Drama" });

            base.OnModelCreating(builder);
        }
    }
}
