namespace Application.DTOs.User;

public class UserUpdateRequestDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
}