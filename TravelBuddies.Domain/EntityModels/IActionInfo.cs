namespace TravelBuddies.Domain.EntityModels
{
	public interface IActionInfo
	{
		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }
	}
}
