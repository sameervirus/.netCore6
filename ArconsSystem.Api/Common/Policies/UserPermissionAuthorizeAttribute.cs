using Microsoft.AspNetCore.Authorization;

namespace ArconsSystem.Api.Common.Policies;

internal class UserPermissionAuthorizeAttribute : AuthorizeAttribute
{
    const string POLICY_PREFIX = "Permission_";

    public UserPermissionAuthorizeAttribute(string permission) => Permission = permission;

    // Get or set the Age property by manipulating the underlying Policy property
    public string? Permission
    {
        get
        {
            if (Policy?[POLICY_PREFIX.Length..] is not null)
            {
                return Policy[POLICY_PREFIX.Length..];
            }
            return default;
        }
        set
        {
            Policy = $"{POLICY_PREFIX}{value}";
        }
    }
}