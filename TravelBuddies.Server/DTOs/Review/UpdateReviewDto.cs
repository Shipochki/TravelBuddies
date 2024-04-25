namespace TravelBuddies.Presentation.DTOs.Review
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Common.DataConstants.ReviewConstants;

    public class UpdateReviewDto
	{
		[Required]
		public int Id { get; set; }

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
