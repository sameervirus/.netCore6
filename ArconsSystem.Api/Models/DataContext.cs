using ArconsSystem.Api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace ArconsSystem.Api.Models;

public class DataContext : DbContext
{
    public DataContext(){}
    public DataContext(DbContextOptions<DataContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>().HasData( new Permission { Id = 1, Name = "GetRoles"});
        modelBuilder.Entity<Permission>().HasData( new Permission { Id = 2, Name = "MakeRoles"});
        modelBuilder.Entity<Role>().HasData( new Role { Id = 1, Name = "Super Admin"});
        modelBuilder.Entity<Role>().HasData( new Role { Id = 2, Name = "Admin"});
        modelBuilder.Entity<Role>().HasData( new Role { Id = 3, Name = "User"});
        modelBuilder.Entity("PermissionRole").HasData( new {RolesId = 1, PermissionsId = 1});
        modelBuilder.Entity("PermissionRole").HasData( new {RolesId = 1, PermissionsId = 2});
        modelBuilder.Entity("PermissionRole").HasData( new {RolesId = 2, PermissionsId = 1});
        modelBuilder.Entity("PermissionRole").HasData( new {RolesId = 2, PermissionsId = 2});
        modelBuilder.Entity("PermissionRole").HasData( new {RolesId = 3, PermissionsId = 1});
        modelBuilder.Entity<User>().HasData(
            new User {
                Id = 1,
                Name = "Samir Nabil",
                Email = "samir@gmail.com",
                Username = "sameervirus",
                Password = "ACKgJf4T3eE3KjVt+kh3KuF+5r3mNObv+LR4VsDuthQcYzrhynrZ7o/xdPvPMrKgNw==",
                RoleId = 1
            }
        );
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
}