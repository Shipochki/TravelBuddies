namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static DataConstants.PostConstants;

	public class Post
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthDestination)]
		public required string FromDestination { get; set; }

		[Required]
		[MaxLength(MaxLengthDestination)]
		public required string ToDestination { get; set;}

		[Required]
		[MaxLength(MaxLengthDescription)]
		public required string Description { get; set; }

		public decimal PriceForSeat { get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }
			
		public bool IsCompleted { get; set; }

		public DateTime DateAndTime { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public int CreatorId { get; set; }

		public required User User { get; set; }

		[ForeignKey(nameof(Group))]
		public int? GroupId { get; set; }
		public Group? Group { get; set; }

		public bool IsDeleted { get; set; } = false;
	}
}
