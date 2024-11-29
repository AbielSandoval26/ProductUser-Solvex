using Application.DTO.Auth;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Auth
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.LoginRequest.Email);
            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.LoginRequest.Password))
            {
                throw new UnauthorizedAccessException("Email o contraseña inválidos.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Id = user.Id, 
                Email = user.Email,
                Token = token,
                Role = user.Rol
            };
        }
    }
}
