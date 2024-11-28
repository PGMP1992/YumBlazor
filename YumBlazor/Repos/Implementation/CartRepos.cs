using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class CartRepos : ICartRepos
    {
        private readonly ApplicationDbContext _db;

        public CartRepos(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ClearCartAsync(string? userId)
        {
            var items = await _db.Carts.Where(u => u.UserId == userId).ToListAsync();
            _db.Carts.RemoveRange(items);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync(string? userId)
        {
            return await _db.Carts.Where(u => u.UserId == userId).Include(u => u.Product).ToListAsync();
        }

        public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            
            var cart = await _db.Carts.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };
                await _db.Carts.AddAsync(cart);
                
            }
            else
            {
                cart.Count += updateBy;
                if (cart.Count <= 0)
                {
                    _db.Carts.Remove(cart);
                }
            }
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
