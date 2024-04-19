namespace TravelBuddies.Application.Review
{
	public class ReviewDto : BaseDto<int>
	{
		public required string CreatorId { get; set; }

		public required string ReciverId { get; set; }

		public string? Text { get; set; }

		public int Rating { get; set; }
	}
}
