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
            if(!_db.Users.Any(u => u.Email == "admin@admin.com"))
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

                IdentityRole role = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                };

                if(!_db.Roles.Any(r => r.Name == "admin"))
                {
                    await _roleManager.CreateAsync(role);
                }

                var password = new PasswordHasher<IdentityUser>();
                var hashedPassword = password.HashPassword(user, "admin123");
                user.PasswordHash = hashedPassword;
                var result = await _userManager.CreateAsync(user);

                var createdUser = await _db.Users.FindAsync(user.Id);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(createdUser, role.Name);
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
