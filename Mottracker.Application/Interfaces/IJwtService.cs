namespace Mottracker.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(int userId, string email, string nome);
}

