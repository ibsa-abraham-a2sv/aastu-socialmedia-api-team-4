﻿using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UserConnectionRepository : IUserConnectionRepository
{
    private readonly AppDBContext _dbContext;

    public UserConnectionRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserConnectionEntity> CreateAsync(UserConnectionEntity entity)
    {
        var userConnection = await _dbContext.Set<UserConnectionEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return userConnection.Entity;
    }

    public async Task<UserConnectionEntity> DeleteAsync(int UserId)
    {
        var userConnection = await _dbContext.Set<UserConnectionEntity>().FindAsync(UserId);

        _dbContext.Set<UserConnectionEntity>().Remove(userConnection);
        await _dbContext.SaveChangesAsync();

        return userConnection;
    }

    public async Task<bool> ExistsAsync(int UserId)
    {
        var userConnection = await _dbContext.UserConnectionMappings.FindAsync(UserId);

        return userConnection != null;
    }

    public async Task<UserConnectionEntity> GetUserConnection(int UserId)
    {
        var userConnection = await _dbContext.UserConnectionMappings.FirstOrDefaultAsync(uc => uc.UserId == UserId);

        return userConnection!;
    }

    public async Task CleanUpMapping(int UserId)
    {
        var userConnection = await _dbContext.UserConnectionMappings.FirstOrDefaultAsync(u => u.UserId == UserId);

        if (userConnection == null)
            return;

        _dbContext.UserConnectionMappings.Remove(userConnection);
        await _dbContext.SaveChangesAsync();

        return;
    }
}