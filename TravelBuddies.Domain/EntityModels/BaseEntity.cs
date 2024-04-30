namespace TravelBuddies.Domain.EntityModels
{
	public abstract class BaseEntity<T> : IActionInfo
	{
		public T? Id { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }
	}
}
