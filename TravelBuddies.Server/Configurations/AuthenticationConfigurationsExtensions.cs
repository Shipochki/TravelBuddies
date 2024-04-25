﻿namespace TravelBuddies.Presentation.Configurations
{
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.IdentityModel.Tokens;
	using System.Text;

	public static class AuthenticationConfigurationsExtensions
	{
		public static IServiceCollection AddToken(this IServiceCollection services, IConfiguration configuration)
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
					ValidAudience = configuration["Jwt:Issuer"],
					ValidIssuer = configuration["Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
			});

			return services;
		}
	}
}
