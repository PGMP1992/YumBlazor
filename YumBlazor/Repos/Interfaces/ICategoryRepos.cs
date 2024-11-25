using YumBlazor.Models;

namespace YumBlazor.Repos.Interfaces
{
    public interface ICategoryRepos
    {
        public Task<Category> CreateAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task<bool> DeleteAsync(int id);
        public Task<Category> GetAsync(int id);
        public Task<IEnumerable<Category>> GetAllAsync();
    }
}
