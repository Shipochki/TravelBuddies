namespace TravelBuddies.Domain.Entities
{
    using TravelBuddies.Domain.EntityModels;

    public class Review : BaseSoftDeleteEntity<int>
	{
		public required string CreatorId { get; set; }
		public required ApplicationUser Creator {  get; set; }

		public int Rating { get; set; }

		public string? Text { get; set; }

		public required string ReciverId { get; set; }
		public required ApplicationUser Reciver { get; set; }
	}
}
