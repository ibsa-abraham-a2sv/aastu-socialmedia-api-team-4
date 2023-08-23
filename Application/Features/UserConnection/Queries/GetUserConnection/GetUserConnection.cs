using Application.DTOs.UserConnection;
using MediatR;

namespace Application.Features.UserConnection.Queries.GetUserConnection;

public class GetUserConnection : IRequest<UserConnectionDto>
{
    public FindUserConnectionDto FindUserConnectionDto { get; set; }
}