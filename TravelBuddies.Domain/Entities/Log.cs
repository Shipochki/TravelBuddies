namespace TravelBuddies.Domain.Entities
{
	using TravelBuddies.Domain.EntityModels;
	using TravelBuddies.Domain.Enums;

	public class Log : BaseEntity<int>
	{
		public required string Message { get; set; }

		public LogLevel LogLevel { get; set; }
	}
}
