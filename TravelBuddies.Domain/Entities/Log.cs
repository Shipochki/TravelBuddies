namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;

	public class Log
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(500)]
		public required string Text { get; set; }

		[Required]
		public DateTime DateTimeLogged { get; set; } = DateTime.Now;

		//UserId null

		//User null
	}
}
