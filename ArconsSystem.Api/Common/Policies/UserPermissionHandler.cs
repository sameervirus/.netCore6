using System.Security.Claims;
using ArconsSystem.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace ArconsSystem.Api.Common.Policies;

internal class UserPermissionHandler : AuthorizationHandler<UserPermission>
{
    private readonly DataContext _db;

    public UserPermissionHandler(DataContext db)
    {
        _db = db;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, UserPermission permission)
    {
        var userRole = context.User.FindFirst(
            c => c.Type == ClaimTypes.AuthorizationDecision &&
            c.Issuer == "Samir Nabil");

        if (userRole is null)
        {
            return Task.CompletedTask;
        }

        var rolePrermissions = _db
            .Permissions
            .Where(p => p.Roles.Any(r => r.Name == userRole.Value))
            .Select(p => p.Name)
            .ToArray();

        if (rolePrermissions.Contains(permission.Permission))
        {
            context.Succeed(permission);
        }

        return Task.CompletedTask;
    }
}