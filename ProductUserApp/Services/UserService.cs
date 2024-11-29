using ProductUserApp.Models;
using ProductUserApp.Services.Interfaces;
using System.Net.Http.Json;

namespace ProductUserApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<User>>("api/User");

        public async Task<User> GetUserByIdAsync(int id) =>
            await _http.GetFromJsonAsync<User>($"api/User/{id}");

        public async Task CreateUserAsync(User user) =>
            await _http.PostAsJsonAsync("api/User", user);

        public async Task UpdateUserAsync(User user) =>
            await _http.PutAsJsonAsync($"api/User/{user.Id}", user);

        public async Task DeleteUserAsync(int id) =>
            await _http.DeleteAsync($"api/User/{id}");

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login", new { email = email, password = password });
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null; 
        }
    }
}
