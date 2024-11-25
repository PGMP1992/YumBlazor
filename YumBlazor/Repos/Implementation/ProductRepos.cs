using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class ProductRepos : IProductRepos
    {
        private readonly ApplicationDbContext _db;

        public ProductRepos(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateAsync(Product Product)
        {
            await _db.Products.AddAsync(Product);
            await _db.SaveChangesAsync();
            return Product;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var Product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (Product != null)
            {
                _db.Products.Remove(Product);
                return (await _db.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var Product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (Product == null)
            {
                return new Product();
            }
            return Product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product Product)
        {
            var fromDb = await _db.Products.FirstOrDefaultAsync(c => c.Id == Product.Id);
            if (fromDb is not null)
            {
                fromDb.Name = Product.Name;
                fromDb.Description = Product.Description;
                fromDb.Price = Product.Price;
                fromDb.CategoryId = Product.CategoryId;
                fromDb.ImageUrl = Product.ImageUrl;

                _db.Products.Update(fromDb);
                await _db.SaveChangesAsync();
                return fromDb;
            }
            return Product;
        }
    }
}
