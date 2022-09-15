using System.ComponentModel.DataAnnotations;

namespace ArconsSystem.Contract.Users;

public record LoginRequest(
    [Required]
    string Username,
    [Required]
    string Password
);