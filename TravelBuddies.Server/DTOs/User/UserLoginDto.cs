namespace TravelBuddies.Presentation.DTOs.User
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Constants.DataConstants.UserConstants;

    public class UserLoginDto
	{
		[Required]
		[MinLength(MinLengthEmail)]
		[MaxLength(MaxLengthEmail)]
		public required string Email { get; set; }

		[Required]
		[MinLength(8)]
		[MaxLength(16)]
		public required string Password { get; set; }
	}
}
