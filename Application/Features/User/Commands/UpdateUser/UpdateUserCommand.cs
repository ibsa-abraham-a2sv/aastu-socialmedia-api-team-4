using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Unit>
{
    public UserRequestDto UserDto { get; set; }
}