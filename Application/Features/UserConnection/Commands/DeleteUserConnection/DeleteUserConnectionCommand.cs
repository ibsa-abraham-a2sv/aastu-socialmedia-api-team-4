using Application.DTOs.UserConnection;
using MediatR;

namespace Application.Features.UserConnection.Commands.DeleteUserConnection;

public class DeleteUserConnectionCommand : IRequest<Unit>
{
    public FindUserConnectionDto FindUserConnectionDto { get; set; }
}