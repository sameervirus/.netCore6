using Microsoft.AspNetCore.Authorization;

namespace ArconsSystem.Api.Common.Policies;

public class UserPermission : IAuthorizationRequirement
{
    public UserPermission(string permission) =>
        Permission = permission;

    public string Permission { get; private set; }
}