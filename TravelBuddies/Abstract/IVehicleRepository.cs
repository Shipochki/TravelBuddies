namespace TravelBuddies.Application.Abstract
{
	using TravelBuddies.Domain.Entities;

	public interface IVehicleRepository
	{
		Task<Vehicle> CreateVehicleAsync(Vehicle vehicle, User user);

		Task<Vehicle?> GetVehicleByOwnerIdAsync(int ownerId);

		Task DeleteVehicle(Vehicle vehicle);

		Task<Vehicle> EditVehicleAsync(Vehicle vehicle);

		Task<Vehicle?> GetVehicleById(int vehicleId);
	}
}
