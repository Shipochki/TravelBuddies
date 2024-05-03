using TravelBuddies.Domain.Entities;

namespace TravelBuddies.Presentation.DTOs.User
{
	public class PostCreatorDto
	{
		public required string Id { get; set; }

		public required string FullName { get; set; }

		public string? ProfilePictureLink { get; set; }

		public static PostCreatorDto FromUser(ApplicationUser user)
		{
			return new PostCreatorDto()
			{
				Id = user.Id,
				FullName = $"{user.FirstName} {user.LastName}",
				ProfilePictureLink = user.ProfilePictureLink
			};
		}
	}
}
