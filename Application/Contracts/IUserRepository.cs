﻿using Application.Contracts.Common;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Contracts;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    // Task<AuthResponse> Login(UserRequestDto user);
    // Task<UserEntity> Register(UserEntity user);
    public Task<UserEntity?> GetUserByEmail(string email);
}