namespace ArconsSystem.Api.Models.Users;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public int RoleId { get; set; } = 0;
    public Role Role { get; set; } = null!;
}

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public int RoleId { get; set; } = 0;
    public string Role { get; set; } = null!;
}