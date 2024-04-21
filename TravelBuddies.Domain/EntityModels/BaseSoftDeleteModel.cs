namespace TravelBuddies.Domain.EntityModels
{
	public class BaseSoftDeleteModel<T> : BaseEntity<T>, ISoftDeleteEntity
	{
		public bool IsDeleted { get; set; }
		public DateTime DeletedOn { get; set; }
	}
}
