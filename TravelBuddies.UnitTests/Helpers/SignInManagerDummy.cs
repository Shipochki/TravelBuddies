namespace TravelBuddies.UnitTests.Helpers
{
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using System.Threading.Tasks;
	using TravelBuddies.Domain.Entities;

	public class SignInManagerDummy : SignInManager<ApplicationUser>
	{
		public SignInManagerDummy(
			UserManager<ApplicationUser> userManager
			, IHttpContextAccessor contextAccessor
			, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory
			, IOptions<IdentityOptions> optionsAccessor
			, ILogger<SignInManager<ApplicationUser>> logger
			, IAuthenticationSchemeProvider schemes
			, IUserConfirmation<ApplicationUser> confirmation)
			: base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
		{
		}

		public override async Task SignInAsync(ApplicationUser user, bool isPersistent, string? authenticationMethod = null)
		{
			await Task.CompletedTask;
		}
	}
}
