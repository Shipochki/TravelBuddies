namespace TravelBuddies.Presentation.DTOs.User
{
	using TravelBuddies.Domain.Entities;

	public class GetOnlyUserByIdDto
	{
		public required string Id { get; set; }

		public required string FirstName { get; set; }

		public required string LastName { get; set; }

		public required string Email { get; set; }

		public string? City { get; set; }

		public string? Country { get; set; }

		public string? ProfilePictureLink { get; set; }

		public static GetOnlyUserByIdDto FromUser(ApplicationUser user)
		{
			return new GetOnlyUserByIdDto()
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				City = user.City,
				Country = user.Country,
				ProfilePictureLink = user.ProfilePictureLink,
			};
		}
	}
}
