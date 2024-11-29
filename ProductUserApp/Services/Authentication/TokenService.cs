using Blazored.LocalStorage;
using ProductUserApp.Services.Interfaces;
using System.Threading.Tasks;

namespace ProductUserApp.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly ILocalStorageService _localStorage;

        public TokenService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        private const string TokenKey = "authToken";

        public async Task<string> GetTokenAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey); 
            return token;
        }

        public async Task SaveTokenAsync(string token)
        {
           await _localStorage.SetItemAsync(TokenKey, token);
        }

        public async Task ClearTokenAsync()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
        }
    }
}




