using Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries.Login
{
    public class LoginQuery : IRequest<AuthResponse>
    {
        public UserRequestDto User { get; set; } = null!;
    }
}
