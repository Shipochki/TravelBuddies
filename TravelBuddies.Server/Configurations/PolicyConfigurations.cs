﻿namespace TravelBuddies.Presentation.Configurations
{
	using TravelBuddies.Presentation.Constants;

	public static class PolicyConfigurations
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
			});

			return service;
		}
	}
}
