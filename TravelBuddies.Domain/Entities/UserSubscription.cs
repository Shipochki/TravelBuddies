namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.ComponentModel.DataAnnotations;

	public class UserSubscription
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public required User User { get; set; }

		public bool IsPaid { get; set; }

		public DateTime DateTimePaid { get; set; } = DateTime.Now;

		public bool IsAutomaticallyPaid { get; set; }
	}
}
