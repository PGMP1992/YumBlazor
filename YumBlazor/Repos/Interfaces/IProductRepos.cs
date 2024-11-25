using YumBlazor.Models;

namespace YumBlazor.Repos.Interfaces
{
    public interface IProductRepos
    {
        public Task<Product> CreateAsync(Product product);
        public Task<Product> UpdateAsync(Product product);
        public Task<bool> DeleteAsync(int id);
        public Task<Product> GetAsync(int id);
        public Task<IEnumerable<Product>> GetAllAsync();
    }
}
