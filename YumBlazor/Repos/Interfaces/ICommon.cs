using Microsoft.AspNetCore.Components.Authorization;

namespace YumBlazor.Repos.Interfaces
{
    public interface ICommon
    {
        Task<bool> IsUserAuthenticated();
        Task<string> GetUserId();
        Task<string> GetUserEmail();
    }
}
