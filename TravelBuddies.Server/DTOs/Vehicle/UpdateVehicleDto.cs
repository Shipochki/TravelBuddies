namespace TravelBuddies.Presentation.DTOs.Vehicle
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Common.DataConstants.VehicleConstants;

    public class UpdateVehicleDto
	{
		public int Id { get; set; }

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

		public int Year { get; set; }

		[Required]
		[MinLength(MinLengthColor)]
		[MaxLength(MinLengthColor)]
		public required string Color { get; set; }

		public int SeatCount { get; set; }

		public IFormFile? PictureLink { get; set; }

		public bool ACSystem { get; set; }
	}
}
