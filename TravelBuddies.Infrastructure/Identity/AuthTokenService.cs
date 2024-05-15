namespace TravelBuddies.Infrastructure.Identity
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Configuration;
	using Microsoft.IdentityModel.Tokens;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Text;
	using TravelBuddies.Application.Common.Interfaces;
	using TravelBuddies.Domain.Entities;

	public class AuthTokenService : IAuthTokenService
	{
		public string GenerateAccessToken(
			ApplicationUser user
			, IConfiguration configuration
			, UserManager<ApplicationUser> userManager)
		{
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			List<Claim> claims = new List<Claim>()
			{

				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.NameId, user.Id),
				new Claim("fullname", $"{user.FirstName} {user.LastName}"),

				// Add additional claims as needed (e.g., roles, custom claims)
			};

			if (user.ProfilePictureLink != null)
			{
				claims.Add(new Claim("profilePictureLink", user.ProfilePictureLink));
			}

			foreach (var role in userManager.GetRolesAsync(user).Result)
			{
				claims.Add(new Claim("role", role));
			}

			var token = new JwtSecurityToken(
				configuration["Jwt:Issuer"],
				configuration["Jwt:Issuer"],
				claims,
				expires: DateTime.UtcNow.AddHours(24),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
