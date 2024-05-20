namespace TravelBuddies.Presentation.DTOs.Review
{
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Presentation.DTOs.User;
	using TravelBuddies.Presentation.Extensions;

	public class ReviewDto
	{
		public int Id { get; set; }

		public string? Text { get; set; }

		public int Rating { get; set; }

		public required string CreatedOn { get; set; }

		public required UserDto Creator { get; set; }


		public static ReviewDto FromReview(Review review)
		{
			return new ReviewDto()
			{
				Id = review.Id,
				Text = review.Text,
				Rating = review.Rating,
				CreatedOn = review.CreatedOn.TimeAgo(),
				Creator = UserDto.FromUser(review.Creator)
			};
		}
	}
}
