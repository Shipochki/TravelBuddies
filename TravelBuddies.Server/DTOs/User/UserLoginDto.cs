namespace TravelBuddies.Presentation.DTOs.User
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Common.DataConstants.UserConstants;

    public class UserLoginDto
	{
		[Required]
		[MinLength(MinLengthEmail)]
		[MaxLength(MaxLengthEmail)]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		[MinLength(MinLengthPassword)]
		[MaxLength(MaxLengthPassword)]
		[DataType(DataType.Password)]
		public required string Password { get; set; }
	}
}
