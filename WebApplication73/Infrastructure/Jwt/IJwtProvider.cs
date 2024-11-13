using WebApplication73.Entities;

namespace WebApplication73.Infrastructure.Jwt
{
    public interface IJwtProvider
    {
        string GenerateToken(User u);
    }
}
