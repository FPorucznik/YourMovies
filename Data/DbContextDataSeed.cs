using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace YourMovies.Data
{
    public class DbContextDataSeed
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbContextDataSeed(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAdminUser()
        {
            if(!_db.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var user = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                if(!_db.Roles.Any(r => r.Name == "admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "ADMIN" });
                }

                var password = new PasswordHasher<IdentityUser>();
                var hashedPassword = password.HashPassword(user, "admin123");
                user.PasswordHash = hashedPassword;
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "admin");
            }
            await _db.SaveChangesAsync();
        }
    }
}
