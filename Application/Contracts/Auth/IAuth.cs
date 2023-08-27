using Application.DTOs.Auth;

namespace Application.Contracts.Auth;

public interface IAuth
{
    public Task<AuthResponse> Login(AuthRequest authRequest);
}