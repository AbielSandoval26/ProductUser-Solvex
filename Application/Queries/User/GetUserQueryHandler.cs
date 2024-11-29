using Application.DTO;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.User
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return new UserDTO();
            }

            return new UserDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Rol = user.Rol,
                PasswordHash = user.PasswordHash,
                FechaCreacion = user.FechaCreacion,
                FechaModificacion = user.FechaModificacion
            };
        }
    }
}
