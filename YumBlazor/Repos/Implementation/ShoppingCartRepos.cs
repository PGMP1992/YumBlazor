using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class ShoppingCartRepos(ApplicationDbContext _db) : IShoppingCartRepos
    {
        //private readonly ApplicationDbContext _db;

        //public ShoppingCartRepos(ApplicationDbContext db)
        //{
        //    _db = db;
        //}

        public async Task<bool> ClearCartAsync(string? userId)
        {
            var items = await _db.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
            _db.ShoppingCarts.RemoveRange(items);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _db.ShoppingCarts.Where(u => u.UserId == userId).Include(u => u.Product).ToListAsync();
        }

        public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);
            if (cart == null)
            {
                cart = new ShoppingCart()
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };
                await _db.ShoppingCarts.AddAsync(cart);
                
            }
            else
            {
                cart.Count += updateBy;
                if (cart.Count <= 0)
                {
                    _db.ShoppingCarts.Remove(cart);
                }
            }
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<int> GetTotalCartAsync(string? userId)
        {
            int cartCount = 0;
            var cart = await _db.ShoppingCarts.Where(u => u.UserId == userId).ToListAsync();
            foreach (var item in cart)
            {
                cartCount += item.Count;
            }
            return cartCount;
        }
    }
}
