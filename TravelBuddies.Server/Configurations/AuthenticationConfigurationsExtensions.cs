namespace TravelBuddies.Presentation.Configurations
{
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.IdentityModel.Tokens;
	using System.Text;

	public static class AuthenticationConfigurationsExtensions
	{
		public static IServiceCollection AddToken(this IServiceCollection services)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidAudience = "https://localhost:7005",
					ValidIssuer = "https://localhost:7005",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ee442f33-e195-4896-85b7-f6ce18bfdcab"))
				};
			});

			return services;
		}
	}
}
