using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository 
    {
        Task<int> AddAsync(Users user);
        Task<Users> GetUserByEmailAsync(string email);
        Task<Users> GetByIdAsync(int id);
        Task<List<Users>> GetAllAsync();
        Task UpdateAsync(Users user);
        Task DeleteAsync(int id);

    }
}
