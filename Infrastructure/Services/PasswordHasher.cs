using Domain.Interfaces;
using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        /*Estoy haciendo el uso directamente de la libreria aqui 
        debido a que el using BCrypt.Net no me esta funcionando*/
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
