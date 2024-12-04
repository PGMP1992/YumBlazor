using YumBlazor.Models;

namespace YumBlazor.Repos.Interfaces
{
    public interface IOrderRepos
    {
        Task<OrderHeader> CreateAsync(OrderHeader orderHeader);
        Task<OrderHeader> GetAsync(int id);
        Task<OrderHeader> GetOrderBySessionIdAsync(string sessionId);
        Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId=null);
        Task<OrderHeader> UpdateStatusAsync(int id, string status, string paymentIntentId);        
    }
}
