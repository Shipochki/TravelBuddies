namespace TravelBuddies.Application.Common.Interfaces
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Configuration;
	using TravelBuddies.Domain.Entities;

	public interface IAuthTokenService
	{
		public string GenerateAccessToken(ApplicationUser user, IConfiguration configuratio, UserManager<ApplicationUser> userManager);
	}
}
