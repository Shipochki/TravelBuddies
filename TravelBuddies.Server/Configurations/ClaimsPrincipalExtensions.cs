namespace TravelBuddies.Presentation.Configurations
{
	using System.Security.Claims;

	public static class ClaimsPrincipalExtensions
	{
		public static string Id(this ClaimsPrincipal user)
		{
			string? result = user.FindFirstValue(ClaimTypes.NameIdentifier);

			return result == null ? string.Empty : result;
		}
	}
}
