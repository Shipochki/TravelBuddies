namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IVehicleRepository
	{
		Task CreateVehicleAsync(Vehicle vehicle);

		void DeleteVehicle(Vehicle vehicle);

		void UpdateVehicleAsync(Vehicle vehicle);

		Task<Vehicle?> GetVehicleById(int vehicleId);
	}
}
