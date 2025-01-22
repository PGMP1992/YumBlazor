using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YumBlazor.Utility;

namespace YumBlazor.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager,
                ApplicationDbContext db
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            string admin = "admin@email.com";
            string pass = "Admin123*";

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            _db.Database.EnsureCreated();

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            }

            // Create Admin User
            _userManager.CreateAsync(new AppUser()
            {
                UserName = admin,
                Email = admin,
                PhoneNumber = "+46 (0) 12345678",

            }, pass).GetAwaiter().GetResult();

            AppUser user = _db.Users.FirstOrDefault(u => u.Email == admin);
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            return;
        }
    }
}