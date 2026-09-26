using Drivex.Domain.Users;
using Drivex.DTOs.Auth;
using Drivex.Repositories.Users;

namespace Drivex.Services.Auth;

public class AuthService : IAuthService
{
    public const string CustomerRole = "Customer";

    // Precomputed bcrypt hash so failed logins still do a verify and avoid user-enumeration timing.
    private const string DummyPasswordHash = "$2a$12$R9h/cIPz0gi.URNNX3kh2OPST9/PgBkqquzi.Ss7KIUgO2t0jWMUW";

    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthOperationResult> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken = default)
    {
        if (!TryValidatePassword(request.Password, out var passwordError))
        {
            return new AuthOperationResult(false, StatusCodes.Status400BadRequest, passwordError, null);
        }

        var email = NormalizeEmail(request.Email);
        var existing = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (existing is not null)
        {
            return new AuthOperationResult(false, StatusCodes.Status409Conflict, "An account with this email already exists.", null);
        }

        var user = new User
        {
            Name = request.Name.Trim(),
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12),
            Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim(),
            Role = CustomerRole
        };

        var created = await _userRepository.CreateAsync(user, cancellationToken);
        return new AuthOperationResult(true, StatusCodes.Status201Created, null, ToAuthResponse(created));
    }

    public async Task<AuthOperationResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = NormalizeEmail(request.Email);
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        var hash = user?.PasswordHash ?? DummyPasswordHash;
        var passwordValid = false;
        try
        {
            passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, hash);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            passwordValid = false;
        }

        if (user is null || !passwordValid)
        {
            return new AuthOperationResult(false, StatusCodes.Status401Unauthorized, "Invalid email or password.", null);
        }

        return new AuthOperationResult(true, StatusCodes.Status200OK, null, ToAuthResponse(user));
    }

    private AuthResponse ToAuthResponse(User user)
    {
        var (token, expiresAt) = _jwtTokenService.CreateAccessToken(user);

        return new AuthResponse
        {
            AccessToken = token,
            TokenType = "Bearer",
            ExpiresAt = expiresAt,
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role
            }
        };
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static bool TryValidatePassword(string password, out string? error)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            error = "Password must be at least 8 characters.";
            return false;
        }

        if (password.Length > 100)
        {
            error = "Password must be at most 100 characters.";
            return false;
        }

        if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
        {
            error = "Password must contain at least one letter and one number.";
            return false;
        }

        error = null;
        return true;
    }
}
