namespace TravelBuddies.Domain.EntityModels
{
	public class BaseSoftDeleteEntity<T> : BaseEntity<T>, ISoftDeleteEntity
	{
		public bool IsDeleted { get; set; }
		public DateTime DeletedOn { get; set; }
	}
}
