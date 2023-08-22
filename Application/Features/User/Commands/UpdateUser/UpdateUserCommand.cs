using Application.DTOs.User;
using MediatR;

namespace Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public UserRequestDto UserDto { get; set; }
}