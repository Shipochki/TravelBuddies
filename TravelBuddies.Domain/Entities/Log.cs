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
		public DateTime DateTimeLogged { get; set; } = DateTime.Now;

		[ForeignKey(nameof(User))]
		public int? UserId { get; set; }
		public User? User { get; set; }
	}
}
