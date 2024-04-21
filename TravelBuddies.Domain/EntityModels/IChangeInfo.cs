namespace TravelBuddies.Domain.EntityModels
{
	public interface IChangeInfo
	{
		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }
	}
}
