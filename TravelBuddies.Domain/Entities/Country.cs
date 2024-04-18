namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using static DataConstants.Country;

	public class Country
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthCountryName)]
		public required string Name { get; set; }
	}
}
