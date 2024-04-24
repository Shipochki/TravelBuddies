namespace TravelBuddies.Presentation.DTOs.Message
{
	using System.ComponentModel.DataAnnotations;
	using static TravelBuddies.Domain.DataConstants.MessageConstants;

	public class UpdateMessageDto
	{
		public int Id { get; set; }

		[Required]
		[MinLength(MinLengthText)]
		[MaxLength(MaxLengthText)]
		public required string Text { get; set; }

		public int GroupId { get; set; }
	}
}
