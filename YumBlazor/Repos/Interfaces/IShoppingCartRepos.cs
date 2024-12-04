using YumBlazor.Models;

namespace YumBlazor.Repos.Interfaces
{
    public interface IShoppingCartRepos
    {
        public Task<bool> UpdateCartAsync(string userId, int productId, int updateBy);
        public Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId);
        public Task<bool> ClearCartAsync( string userId);
        public Task<int> GetTotalCartAsync(string? userId);
    }
}
