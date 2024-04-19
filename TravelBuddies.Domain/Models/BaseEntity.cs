namespace TravelBuddies.Domain.Models
{
	using System.ComponentModel.DataAnnotations;

	public abstract class BaseEntity<T>
	{
		[Key]
		public T? Id { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }
	}
}
