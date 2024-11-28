using YumBlazor.Models;

namespace YumBlazor.Repos.Interfaces
{
    public interface ICartRepos
    {
        public Task<bool> UpdateCartAsync(string userId, int productId, int updateBy);
        public Task<IEnumerable<Category>> GetAllAsync(string? userId);
        public Task<bool> ClearCartAsync( string userId);
    }
}
