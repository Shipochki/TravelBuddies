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

		[Required]
		public decimal PriceForSeat { get; set; }

		[Required]
		public int FreeSeats { get; set; }

		[Required]
		public bool Baggage { get; set; }

		[Required]
		public bool Pets { get; set; }
			
		public bool IsCompleted { get; set; }

		public DateTime DateAndTime { get; set; }

		[Required]
		[ForeignKey(nameof(Creator))]
		public int CreatorId { get; set; }
		public required User Creator { get; set; }

		[ForeignKey(nameof(Group))]
		public int? GroupId { get; set; }
		public Group? Group { get; set; }

		public bool IsDeleted { get; set; } = false;
	}
}
