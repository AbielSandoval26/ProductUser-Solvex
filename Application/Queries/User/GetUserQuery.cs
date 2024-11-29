using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.User
{
    public class GetUserQuery : IRequest<UserDTO>
    {
        public int Id { get; set; }

        public GetUserQuery(int id)
        {
            Id = id;
        }
    }

}
