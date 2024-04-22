namespace TravelBuddies.Application.User
{
	public class ApplicationUserDto : BaseDto<string>
	{
		public required string Email { get; set; }

		public required string Password { get; set; }

		public required string FirstName { get; set; }

		public required string LastName { get; set; }

		public string? Country { get; set; }

		public string? City { get; set; }
	}
}
