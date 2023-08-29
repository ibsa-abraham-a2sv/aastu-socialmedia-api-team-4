using Application.DTOs.UserConnection;
using Domain.Entities;

namespace Application.Contracts;

public interface IUserConnectionRepository
{
    public Task<UserConnectionEntity> CreateAsync(UserConnectionEntity entity);
    public Task<UserConnectionEntity> DeleteAsync(int UserId);
    public Task<bool> ExistsAsync(int UserId);
    public Task<UserConnectionEntity> GetUserConnection(int UserId);
    public Task CleanUpMapping(int UserId);
}