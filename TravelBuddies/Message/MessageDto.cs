namespace TravelBuddies.Application.Message
{
	public class MessageDto : BaseDto<int>
	{
		public required string Text { get; set; }

		public required string CreatorId { get; set; }

		public int GroupId { get; set; }
	}
}
