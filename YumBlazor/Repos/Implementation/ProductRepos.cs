using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class ProductRepos(ApplicationDbContext _db
        ,IPhotoService _photoService) : IProductRepos
    {
        //private readonly ApplicationDbContext _db;
        ////private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IPhotoService _photoService;

        //public ProductRepos(ApplicationDbContext db
        //    //, IWebHostEnvironment webHostEnvironment
        //    , IPhotoService photoService)
        //{
        //    _db = db;
        //    //_webHostEnvironment = webHostEnvironment;
        //    _photoService = photoService;
        //}

        public async Task<Product> CreateAsync(Product Product)
        {
            await _db.Products.AddAsync(Product);
            await _db.SaveChangesAsync();
            return Product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (product == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                //var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                //if (File.Exists(imagePath))
                //{
                //    File.Delete(imagePath);
                //}
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    _ = _photoService.DeletePhotoAsync(product.ImageUrl);
                    product.ImageUrl = null;
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
            var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null)
            {
                return new Product();
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products
                .AsNoTracking()
                .Include(u => u.Category)
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
                fromDb.Tag = Product.Tag;
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
