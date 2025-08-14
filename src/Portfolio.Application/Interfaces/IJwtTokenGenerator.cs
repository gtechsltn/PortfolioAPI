
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
