namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using static DataConstants.ReviewConstants;

	public class Review
	{
		[Key]
		public int Id { get; set; }

		//CreatorId

		//Creator User

		public int Rating { get; set; }

		[MaxLength(MaxLengthText)]
		public string? Text { get; set; }

		//ReciverId

		//Reciver User

		public bool IsDeleted { get; set; }
	}
}
