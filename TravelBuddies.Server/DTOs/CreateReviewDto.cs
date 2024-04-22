namespace TravelBuddies.Presentation.DTOs
{
	using System.ComponentModel.DataAnnotations;
	using static TravelBuddies.Domain.DataConstants.ReviewConstants;

	public class CreateReviewDto
	{
		public required string CreatorId { get; set; }

		public required string ReciverId { get; set; }

		[MinLength(MinLengthText)]
		[MaxLength(MaxLengthText)]
		public string? Text { get; set; }

		[Required]
		[Range(1, 5)]
		public int Rating { get; set; }
	}
}
