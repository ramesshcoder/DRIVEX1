using Drivex.DTOs.Auth;

namespace Drivex.Services.Auth;

public interface IAuthService
{
    Task<AuthOperationResult> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken = default);
    Task<AuthOperationResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}

public record AuthOperationResult(bool Succeeded, int StatusCode, string? Error, AuthResponse? Data);
