namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static DataConstants.UserConstants;

	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthFirstName)]
		public required string FirstName { get; set; }

		[Required]
		[MaxLength(MaxLengthLastName)]
		public required string LastName { get; set; }

		[Required]
		[MaxLength(MaxLengthPhoneNumber)]
		public required string PhoneNumber { get; set; }

		[Required]
		[MaxLength(MaxLengthEmail)]
		public required string Email { get; set; }

		public string? ProfilePictureLink { get; set; }

		[Required]
		public required string PasswordHashed { get; set; }

		public HashSet<byte> PasswordSalt { get; set; } = new HashSet<byte>();

		[MaxLength(MaxLengthCountryName)]
		public string? Country { get; set; }

		[MaxLength(MaxLengthCityName)]
		public string? City { get; set; }

		public bool IsEmailConfirmed { get; set; } = false;

		public bool IsSubscriptionPaid { get; set; } = false;

		[ForeignKey(nameof(Vehicle))]
		public int? VehicleId { get; set; }
		public Vehicle? Vehicle { get; set; }

		[Required]
		[ForeignKey(nameof(Role))]
		public int RoleId { get; set; }
		public required Role Role { get; set; }

		public HashSet<Review> Reviews { get; set; } = new HashSet<Review>();

		public HashSet<Message> Messages { get; set; } = new HashSet<Message>();

		public HashSet<UserGroup> UsersGroups { get; set; } = new HashSet<UserGroup>();

		public HashSet<Log> Logs { get; set; } = new HashSet<Log>();

		public HashSet<Post> Posts { get; set; } = new HashSet<Post>();

		public bool IsDeleted { get; set; } = false;
	}
}
