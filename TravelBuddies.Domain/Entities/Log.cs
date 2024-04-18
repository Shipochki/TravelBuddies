namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Log
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(500)]
		public required string Text { get; set; }

		[Required]
		public DateTime LogDateTime { get; set; } = DateTime.Now;

		public string? UserId { get; set; }
	}
}
