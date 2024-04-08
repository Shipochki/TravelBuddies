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

		public List<byte> CodeSalt { get; set; } = new List<byte>();

		public bool IsVerified { get; set; }

		[Required]
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }

		public required User User { get; set; }
	}
}
