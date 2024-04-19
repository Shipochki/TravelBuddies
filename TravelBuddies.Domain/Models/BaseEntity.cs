namespace TravelBuddies.Domain.Models
{
	using System.ComponentModel.DataAnnotations;

	public abstract class BaseEntity<T>
	{
		[Key]
		public required T Id { get; set; }

		public DateTime CreateOn { get; set; }

		public DateTime? UpdatedOn { get; set; }
	}
}
