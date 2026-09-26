using Drivex.Domain.Users;

namespace Drivex.Services.Auth;

public interface IJwtTokenService
{
    (string Token, DateTimeOffset ExpiresAt) CreateAccessToken(User user);
}
