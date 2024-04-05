namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.ComponentModel.DataAnnotations;
	using static DataConstants.VerificationEmail;

	public class VerificationEmail
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthEmail)]
		public required string Email { get; set; }

		public required string VerificationCodeHashed { get; set; }

		public required string CodeSalt { get; set; }

		public bool IsVerified { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }

		public User User { get; set; } = null!;
	}
}
