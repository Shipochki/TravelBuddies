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

		public List<byte> PasswordSalt { get; set; } = new List<byte>();

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

		public List<Review> RecivedReviews { get; set; } = new List<Review>();

		public List<Review> CreatedReviews { get; set; } = new List<Review>();

		public List<Message> Messages { get; set; } = new List<Message>();

		public List<UserGroup> UsersGroups { get; set; } = new List<UserGroup>();

		public List<Log> Logs { get; set; } = new List<Log>();

		public List<Post> Posts { get; set; } = new List<Post>();

		public List<Group> Groups { get; set; } = new List<Group>();

		public bool IsDeleted { get; set; } = false;
	}
}
