using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ArconsSystem.Api.Common.Policies;

internal class UserPermissionPolicyProvider : IAuthorizationPolicyProvider
{
    const string POLICY_PREFIX = "Permission_";
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public UserPermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var permission = policyName[POLICY_PREFIX.Length..];
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new UserPermission(permission));
                return Task.FromResult(policy.Build())!;
            }

            // If the policy name doesn't match the format expected by this policy provider,
            // try the fallback provider. If no fallback provider is used, this would return 
            // Task.FromResult<AuthorizationPolicy>(null) instead.
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
}