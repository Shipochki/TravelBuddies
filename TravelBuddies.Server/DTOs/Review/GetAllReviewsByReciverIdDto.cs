namespace TravelBuddies.Presentation.DTOs.Review
{
	using TravelBuddies.Domain.Entities;

	public class GetAllReviewsByReciverIdDto
	{
		public int Id { get; set; }

		public required string CreatorId { get; set; }

		public required string ReciverId { get; set; }

		public string? Text { get; set; }

		public int Rating { get; set; }

		public static GetAllReviewsByReciverIdDto FromReview(Review review)
		{
			return new GetAllReviewsByReciverIdDto()
			{
				Id = review.Id,
				CreatorId = review.CreatorId,
				ReciverId = review.ReciverId,
				Text = review.Text,
				Rating = review.Rating,
			};
		}
	}
}
