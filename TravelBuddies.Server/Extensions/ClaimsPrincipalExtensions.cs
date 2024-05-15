namespace TravelBuddies.Presentation.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            string? result = user.Claims.ToArray()[2].Value;

            return result == null ? string.Empty : result;
        }
    }
}
