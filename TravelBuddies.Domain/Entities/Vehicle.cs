namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using TravelBuddies.Domain.Enums;
	using static DataConstants.VehicleConstants;

	public class Vehicle
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(MaxLengthBrandName)]
		public required string BrandName { get; set; }

		[Required]
		[MaxLength(MaxLengthModelName)]
		public required string ModelName { get; set; }

		public Fuel Fuel { get; set; }

		public int SeatCount { get; set; }

		public string? PictureLink { get; set; }

		public bool ACSystem { get; set; }
	}
}
