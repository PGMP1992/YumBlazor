using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class OrderRepos : IOrderRepos
    {
        private readonly ApplicationDbContext _db;

        public OrderRepos(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.Now;
            await _db.OrderHeaders.AddAsync(orderHeader);
            await _db.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _db.OrderHeaders.Where(u => u.UserId == userId).ToListAsync();
            }
            return await _db.OrderHeaders.ToListAsync();
        }

        public async Task<OrderHeader> GetAsync(int id)
        {
            return await _db.OrderHeaders.Include(u => u.OrderDetails).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<OrderHeader> UpdateStatusAsync(int id, string status, string paymentIntentId)
        {
            var orderHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if (orderHeader != null)
            {
                orderHeader.Status = status;
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    orderHeader.PaymentIntentId = paymentIntentId;
                }

                await _db.SaveChangesAsync();
            }
            return orderHeader;
        }

        public async Task<OrderHeader> GetOrderBySessionIdAsync(string sessionId)
        {
            return await _db.OrderHeaders.FirstOrDefaultAsync(u => u.SessionId == sessionId);
        }
    }
}
