using Application.DTOs.UserConnection;
using MediatR;

namespace Application.Features.UserConnection.Commands;

public class CreateUserConnectionCommand : IRequest<UserConnectionDto>
{
    public CreateUserConnectionDto CreateUserConnectionDto { get; set; }
}