using TravelBuddies.Domain.Entities;

namespace TravelBuddies.Presentation.DTOs.User
{
	public class UserDto
	{
		public required string Id { get; set; }

		public required string FullName { get; set; }

		public string? ProfilePictureLink { get; set; }

		public static UserDto FromUser(ApplicationUser user)
		{
			return new UserDto()
			{
				Id = user.Id,
				FullName = $"{user.FirstName} {user.LastName}",
				ProfilePictureLink = user.ProfilePictureLink
			};
		}
	}
}
