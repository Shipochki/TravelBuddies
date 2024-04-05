namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using static DataConstants.RoleConstants;

	public class Role
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthName)]
		public required string Name { get; set; }

		//List Users
	}
}
