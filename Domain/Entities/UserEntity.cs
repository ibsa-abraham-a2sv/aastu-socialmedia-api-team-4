using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class UserEntity : BaseDomainEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Bio { get; set; }
    public string Email { get; set; } = null!;
    public string? Token { get; set; } = "";
    public bool IsVerified { get; set; } = false;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? ProfilePicture { get; set; }
    public string? ConfirmationCode { get; set; }
    public DateTime? ConfirmationCodeExpiration { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    public List<UserEntity> Followers { get; set; } = new List<UserEntity>();
    public List<UserEntity> Following { get; set; } = new List<UserEntity>();
    public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    public List<PostEntity> Posts { get; set; } = new List<PostEntity>();
    public List<NotificationEntity> Notifications { get; set; } = new List<NotificationEntity>();
    public List<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
}