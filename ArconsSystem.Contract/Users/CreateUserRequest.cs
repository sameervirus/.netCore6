using System.ComponentModel.DataAnnotations;

namespace ArconsSystem.Contract.Users;

public record CreateUserRequest(
    [Required]
    string Name,
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    string Username,
    [Required]
    string Password,
    int Role
);