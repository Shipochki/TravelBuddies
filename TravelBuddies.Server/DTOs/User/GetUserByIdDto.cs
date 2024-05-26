using TravelBuddies.Domain.Entities;
using TravelBuddies.Presentation.DTOs.Review;
using TravelBuddies.Presentation.DTOs.Vehicle;

namespace TravelBuddies.Presentation.DTOs.User
{
	public class GetUserByIdDto
	{
		public required string Id { get; set; }

		public required string FirstName { get; set; }

		public required string LastName { get; set; }

		public required string Email { get; set; }

		public string? City { get; set; }

		public string? Country { get; set; }

		public string? ProfilePictureLink { get; set; }

		public double? Rating { get; set; }

		public List<ReviewDto>? Reviews { get; set; }

		public VehicleDto? Vehicle { get; set; }

		public static GetUserByIdDto FromUser(ApplicationUser user)
		{
			return new GetUserByIdDto()
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
