using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class ProductRepos : IProductRepos
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepos(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;

            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Product> CreateAsync(Product Product)
        {
            await _db.Products.AddAsync(Product);
            await _db.SaveChangesAsync();
            return Product;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            
            if (product.ImageUrl != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            if (product != null)
            {
                _db.Products.Remove(product);
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
            return await _db.Products.Include(u => u.Category)
                .OrderBy(u => u.Name).ToListAsync();
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
