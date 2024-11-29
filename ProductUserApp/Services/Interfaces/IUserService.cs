using ProductUserApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductUserApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> AuthenticateAsync(string email, string password); 
    }
}
