using Microsoft.AspNetCore.Http;

namespace TravelBuddies.Application.Vehicle
{
	public class VehicleDto : BaseDto<int>
	{
		public required string BrandName { get; set; }

		public required string ModelName { get; set; }

		public int Fuel { get; set; }

		public int SeatCount { get; set; }

		public IFormFile? PictureLink { get; set; }

		public bool ACSystem { get; set; }

		public required string OwnerId { get; set; }
	}
}
