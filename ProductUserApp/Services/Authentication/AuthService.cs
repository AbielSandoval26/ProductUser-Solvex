using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProductUserApp.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductUserApp.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(ITokenService tokenService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _tokenService = tokenService;
            _navigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            var token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return false;

            var claims = GetClaimsFromToken(token);
            return claims.Any(c => c.Type == ClaimTypes.Role && c.Value == role);
        }

        private IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null) return Enumerable.Empty<Claim>();

            return jwtToken?.Claims;
        }

        public async Task<string> GetCurrentUserRoleAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not { IsAuthenticated: true })
            {
                return null; 
            }

            var roleClaim = user.FindFirst(ClaimTypes.Role);
            return roleClaim?.Value;
        }

        public async Task LogoutAsync()
        {
            await _tokenService.ClearTokenAsync();

            _navigationManager.NavigateTo("/login");
        }
    }

}
