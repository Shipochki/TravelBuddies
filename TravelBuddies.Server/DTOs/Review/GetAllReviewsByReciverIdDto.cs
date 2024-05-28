namespace TravelBuddies.Presentation.DTOs.Review
{
	using TravelBuddies.Domain.Entities;

	public class GetAllReviewsByReciverIdDto
	{
		public required string ReciverId { get; set; }

		public required string ReciverFullName { get; set; }

		public int CountReviews { get; set; }

		public List<ReviewDto> Reviews { get; set; }

		public static GetAllReviewsByReciverIdDto FromUser(ApplicationUser user)
		{
			return new GetAllReviewsByReciverIdDto()
			{
				ReciverId = user.Id,
				ReciverFullName = $"{user.FirstName} {user.LastName}",
			};
		}
	}
}
