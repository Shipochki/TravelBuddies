namespace TravelBuddies.Presentation.DTOs.Vehicle
{
	using TravelBuddies.Domain.Entities;

	public class VehicleDto
	{
		public int Id { get; set; }

		public required string BrandName { get; set; }

		public required string ModelName { get; set; }

		public int Year { get; set; }
		public required string Color { get; set; }

		public required string Fuel { get; set; }

		public int SeatCount { get; set; }

		public required string PictureLink { get; set; }

		public bool ACSystem { get; set; }

		public required string OwnerId { get; set; }

		public static VehicleDto FromVehicle(Vehicle vehicle)
		{
			return new VehicleDto()
			{
				Id = vehicle.Id,
				BrandName = vehicle.BrandName,
				ModelName = vehicle.ModelName,
				Year = vehicle.Year,
				Color = vehicle.Color,
				Fuel = vehicle.Fuel.ToString(),
				SeatCount = vehicle.SeatCount,
				PictureLink = vehicle.PictureLink,
				ACSystem = vehicle.ACSystem,
				OwnerId = vehicle.OwnerId
			};
		}
	}
}
