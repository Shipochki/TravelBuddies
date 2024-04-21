namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using TravelBuddies.Domain.EntityModels;
	using static DataConstants.City;

	public class City : BaseSoftDeleteModel<int>
	{
		[Required]
		[MaxLength(MaxLengthCityName)]
		public required string Name { get; set; }

		[Required]
		[ForeignKey(nameof(Country))]
		public int CountryId { get; set; }
		public required Country Country { get; set; }
	}
}
