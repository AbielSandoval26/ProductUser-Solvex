using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _passwordHasher.HashPassword(request.PasswordHash);

            string rol = String.IsNullOrEmpty(request.Rol) && String.IsNullOrWhiteSpace(request.Rol) ? "User" : request.Rol;

            var user = new Users
            {
                Nombre = request.Nombre,
                Email = request.Email,
                PasswordHash = passwordHash,
                Rol = rol,
                FechaCreacion = DateTime.UtcNow
            };

            return await _userRepository.AddAsync(user);
        }
    }
}
