namespace TravelBuddies.Presentation.DTOs.Vehicle
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Constants.DataConstants.VehicleConstants;

    public class CreateVehicleDto
	{
		[Required]
		[MinLength(MinLengthBrandName)]
		[MaxLength(MaxLengthBrandName)]
		public required string BrandName { get; set; }

		[Required]
		[MinLength(MinLengthModelName)]
		[MaxLength(MaxLengthModelName)]
		public required string ModelName { get; set; }

		[Range(MinRangeFule, MaxRangeFuel)]
		public int Fuel { get; set; }

		public int SeatCount { get; set; }

		[Required]
		public required string PictureLink { get; set; }

		public bool ACSystem { get; set; }

		[Required]
		public required string OwnerId { get; set; }
	}
}
