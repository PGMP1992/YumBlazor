using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using YumBlazor.Repos.Interfaces;

namespace YumBlazor.Repos.Implementation
{
    public class Common : ICommon
    {
        private AuthenticationStateProvider _AuthenticationStateProvider;

        public Common(AuthenticationStateProvider authenticationStateProvider)
        {
            _AuthenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var authState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var authenticated = user.Identity is not null && user.Identity.IsAuthenticated;
            return authenticated;
        }

        public async Task<string> GetUserId()
        {
            var authState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
        }

        public async Task<string> GetUserEmail()
        {
            var authState = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(u => u.Type.Contains("email"))?.Value;
        }
    }
}
