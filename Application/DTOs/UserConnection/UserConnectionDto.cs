using Domain.Entities;

namespace Application.DTOs.UserConnection;

public class UserConnectionDto
{
    public int UserId { get; set; }
    public string? ConnectionId { get; set; }
    public UserEntity? UserEntity { get; set; }
}