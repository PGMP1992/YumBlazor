using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class CategoryRepos : ICategoryRepos
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepos(ApplicationDbContext db)
        {
            _db = db;
        }

        public Category Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        public Category Create(CategoryRepos category)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                return _db.SaveChanges() > 0;
            }
            return false;
        }

        public Category Get(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return new Category();
            }
            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category Update(Category category)
        {
            var fromDb = _db.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (fromDb is not null)
            {
                fromDb.Name = category.Name;
                _db.Categories.Update(fromDb);
                _db.SaveChanges();
                return fromDb;
            }
            return category;
        }
    }
}
