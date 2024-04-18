namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static DataConstants.City;

	public class City
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthCityName)]
		public required string Name { get; set; }

		[Required]
		[ForeignKey(nameof(Country))]
		public int CountryId { get; set; }
		public required Country Country { get; set; }
	}
}
