using System.Security.Cryptography;
using ArconsSystem.Api.Models;
using ArconsSystem.Api.Models.Users;
using ArconsSystem.Contract.Users;
using Microsoft.AspNetCore.Mvc;
using ArconsSystem.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ArconsSystem.Api.Controllers;

public class UsersController : ApiController
{
    private readonly DataContext _db;
    private readonly ITokenService _tokenService;

    public UsersController(DataContext db, ITokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<UserDTO> GetUsers()
    {
        var users = from b in _db.Users
                select new UserDTO()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Email = b.Email,
                    Username = b.Username,
                    RoleId = b.RoleId,
                    Role = b.Role.Name
                };

        return users;
    }

    [HttpPost]
    public IActionResult CreateUser(CreateUserRequest request)
    {
        if (_db.Users.FirstOrDefault(u => u.Username == request.Username) is not null)
        {
            return Problem(400, "User already exists");
        }

        var roleId = 0;

        if(request.Role != 0) roleId = request.Role;

        if(_db.Roles.FirstOrDefault(r => r.Id == roleId) is null) {
            return Problem(400, "Role not found");
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Username = request.Username,
            Password = HashPassword(request.Password),
            RoleId = roleId
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        var role = _db.Roles.FirstOrDefault(r => r.Id == user.RoleId)?.Name ?? "user";

        var token = _tokenService.GenerateToken(user, role);

        return Ok(MapResult(user, token));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        if(_db.Users.FirstOrDefault(u => u.Username == loginRequest.Username) is not User user)
        {
            return Problem(401, "Invalid Credentials");
        }

        if(!VerifyHashedPassword(user.Password, loginRequest.Password))
        {
            return Problem(401, "Invalid Credentials");
        }

        var role = _db.Roles.FirstOrDefault(r => r.Id == user.RoleId)?.Name ?? "user";

        var token = _tokenService.GenerateToken(user, role);
        return Ok(MapResult(user, token));
    }

    private static AuthenticationResult MapResult(User user, string token)
    {
        return new AuthenticationResult(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Username,
                    user.Role.Name,
                    token
                );
    }

    public static string HashPassword(string password)
    {
        byte[] salt;
        byte[] buffer2;
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        using (Rfc2898DeriveBytes bytes = new(password, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }
        byte[] dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }

    public static bool VerifyHashedPassword(string hashedPassword, string password)
    {
        byte[] buffer4;
        if (hashedPassword == null)
        {
            return false;
        }
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        byte[] src = Convert.FromBase64String(hashedPassword);
        if ((src.Length != 0x31) || (src[0] != 0))
        {
            return false;
        }
        byte[] dst = new byte[0x10];
        Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        byte[] buffer3 = new byte[0x20];
        Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        using (Rfc2898DeriveBytes bytes = new(password, dst, 0x3e8))
        {
            buffer4 = bytes.GetBytes(0x20);
        }
        return ByteArraysEqual(buffer3, buffer4);
    }

    private static bool ByteArraysEqual(byte[] b1, byte[] b2)
    {
        if (b1 == b2) return true;
        if (b1 == null || b2 == null) return false;
        if (b1.Length != b2.Length) return false;
        for (int i=0; i < b1.Length; i++)
        {
            if (b1[i] != b2[i]) return false;
        }
        return true;
    }
}