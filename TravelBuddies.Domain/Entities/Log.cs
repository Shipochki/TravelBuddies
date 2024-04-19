namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using TravelBuddies.Domain.Models;

	public class Log : BaseEntity<int>
	{
		[Required]
		[MaxLength(500)]
		public required string Message { get; set; }

		[Required]
		public DateTime LogDateTime { get; set; } = DateTime.Now;
	}
}
