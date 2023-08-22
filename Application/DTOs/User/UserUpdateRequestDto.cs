namespace Application.DTOs.User;

public class UserUpdateRequestDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
}