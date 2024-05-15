namespace TravelBuddies.Presentation.Extensions
{
    using TravelBuddies.Domain.Common;

    public static class AuthorizationConfigurationsExtensions
    {
        public static IServiceCollection PolicyConfigure(this IServiceCollection service)
        {
            service.AddAuthorization(options =>
            {
                options.AddPolicy(ApplicationPolicies.EveryUser, policy =>
                {
                    policy.RequireRole(ApplicationRoles.Client, ApplicationRoles.Driver, ApplicationRoles.Admin);
                });
                options.AddPolicy(ApplicationPolicies.OnlyClient, policy =>
                {
                    policy.RequireRole(ApplicationRoles.Client);
                });
                options.AddPolicy(ApplicationPolicies.ClientAndDriver, policy =>
                {
                    policy.RequireRole(ApplicationRoles.Client, ApplicationRoles.Driver);
                });
                options.AddPolicy(ApplicationPolicies.OnlyDriver, policy =>
                {
                    policy.RequireRole(ApplicationRoles.Driver);
                });
                options.AddPolicy(ApplicationPolicies.DriverAndAdmin, policy =>
                {
                    policy.RequireRole(ApplicationRoles.Driver, ApplicationRoles.Admin);
                });
            });

            return service;
        }
    }
}
