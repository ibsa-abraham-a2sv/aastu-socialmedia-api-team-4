﻿using Application.DTOs.Common;

namespace Application.DTOs.User;

public class UserDto : BaseDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
}