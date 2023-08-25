namespace Application.Contracts.Common;

public interface IJwtTokenGenerator
{
    string GenerateToken(int id, string email, string username, string firstname, string lastname);
}