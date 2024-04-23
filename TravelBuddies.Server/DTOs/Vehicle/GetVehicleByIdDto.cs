namespace TravelBuddies.Presentation.DTOs.Vehicle
{
	using TravelBuddies.Domain.Entities;

	public class GetVehicleByIdDto
	{
		public int Id { get; set; }

		public required string BrandName { get; set; }

		public required string ModelName { get; set; }

		public required string Fuel { get; set; }

		public int SeatCount { get; set; }

		public required string PictureLink { get; set; }

		public bool ACSystem { get; set; }

		public required string OwnerId { get; set; }

		public static GetVehicleByIdDto FromVehicle(Vehicle vehicle)
		{
			return new GetVehicleByIdDto()
			{
				Id = vehicle.Id,
				BrandName = vehicle.BrandName,
				ModelName = vehicle.ModelName,
				Fuel = vehicle.Fuel.ToString(),
				SeatCount = vehicle.SeatCount,
				PictureLink = vehicle.PictureLink,
				ACSystem = vehicle.ACSystem,
				OwnerId = vehicle.OwnerId
			};
		}
	}
}
