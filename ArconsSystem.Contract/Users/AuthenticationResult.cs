namespace ArconsSystem.Contract.Users;

public record AuthenticationResult(
    int Id,
    string Name,
    string Email,
    string Username,
    string Role,
    string Token
);