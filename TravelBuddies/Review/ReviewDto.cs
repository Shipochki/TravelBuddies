namespace TravelBuddies.Application.Review
{
	public class ReviewDto
	{
		public int Id { get; set; }

		public int CreatorId { get; set; }

		public int ReciverId { get; set; }

		public string? Text { get; set; }

		public int Rating { get; set; }
	}
}
