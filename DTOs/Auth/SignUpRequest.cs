using System.ComponentModel.DataAnnotations;

namespace Drivex.DTOs.Auth;

public class SignUpRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [MaxLength(20)]
    [Phone]
    public string? Phone { get; set; }
}
