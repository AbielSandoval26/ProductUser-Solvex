using Application.DTO.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Auth
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public LoginRequest LoginRequest { get; }

        public LoginQuery(LoginRequest loginRequest)
        {
            LoginRequest = loginRequest;
        }
    }
}
