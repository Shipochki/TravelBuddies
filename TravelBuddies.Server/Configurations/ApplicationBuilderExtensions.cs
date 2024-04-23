namespace TravelBuddies.Presentation.Configurations
{
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Domain.Entities;

	public static class ApplicationBuilderExtensions
	{
		public static async Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
		{
			using IServiceScope scopedService = app.ApplicationServices.CreateScope();

			IServiceProvider service = scopedService.ServiceProvider;

			var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

			// Ensure "client" role exists
			if (!await roleManager.RoleExistsAsync(ApplicationRoles.Client))
			{
				await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Client));
			}

			// Ensure "driver" role exists
			if (!await roleManager.RoleExistsAsync(ApplicationRoles.Driver))
			{
				await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Driver));
			}

			// Ensure "admin" role exists
			if (!await roleManager.RoleExistsAsync(ApplicationRoles.Admin))
			{
				await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin));
			}

			if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
			{
				ApplicationUser applicationUser = new ApplicationUser()
				{
					UserName = "admin@gmail.com",
					Email = "admin@gmail.com",
					FirstName = "Admin",
					LastName = "Administrator"
				};

				await userManager.CreateAsync(applicationUser, "Password0!");

				await userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Admin);
			}

			return app;
		}
	}
}
