namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using TravelBuddies.Domain.EntityModels;

	public class Log : BaseEntity<int>
	{
		[Required]
		[MaxLength(500)]
		public required string Message { get; set; }
	}
}
