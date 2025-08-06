using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class CategoryRepos(ApplicationDbContext _db) : ICategoryRepos
    {
        //private readonly ApplicationDbContext _db;

        //public CategoryRepos(ApplicationDbContext db)
        //{
        //    _db = db;
        //}

        public async Task<Category> CreateAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                return (await _db.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return new Category();
            }
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var fromDb = await _db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (fromDb is not null)
            {
                fromDb.Name = category.Name;
                _db.Categories.Update(fromDb);
                await _db.SaveChangesAsync();
                return fromDb;
            }
            return category;
        }
    }
}
