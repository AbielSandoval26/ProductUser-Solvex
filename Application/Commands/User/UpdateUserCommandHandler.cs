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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                return false;

            string rol = String.IsNullOrEmpty(request.Rol) && String.IsNullOrWhiteSpace(request.Rol) ? "User" : request.Rol;


            user.Nombre = request.Nombre ?? user.Nombre;
            user.Email = request.Email ?? user.Email;
            user.Rol = rol;

            if (!string.IsNullOrEmpty(request.PasswordHash))
            {
                user.PasswordHash = _passwordHasher.HashPassword(request.PasswordHash);
            }

            user.FechaModificacion = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);

            return true;
        }

        
    }
}
