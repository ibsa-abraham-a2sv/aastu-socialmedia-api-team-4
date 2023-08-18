using Domain.Common;

namespace Domain.Entities;

public class UserEntity : BaseDomainEntity
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
}