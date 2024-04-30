namespace TravelBuddies.Domain.Entities
{
    using TravelBuddies.Domain.EntityModels;

    public class Message : BaseSoftDeleteEntity<int>
	{
		public required string Text { get; set; }

		public required string CreatorId { get; set; }
		public required ApplicationUser Creator {  get; set; }

		public int GroupId { get; set; }
		public required Group Group { get; set; }
	}
}
