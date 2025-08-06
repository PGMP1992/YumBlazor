using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YumBlazor.Models;

namespace YumBlazor.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Product> Products { get; set; }
        public required DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public required DbSet<OrderHeader> OrderHeaders { get; set; }
        public required DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Starter" },
                new Category { Id = 2, Name = "Main" },
                new Category { Id = 3, Name = "Dessert" },
                new Category { Id = 4, Name = "Drinks" }

            );
        }
    }
}
