namespace TravelBuddies.Infrastructure.Repository
{
	using Microsoft.EntityFrameworkCore;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Abstract;
	using TravelBuddies.Domain.Entities;

	public class VehicleRepository : IVehicleRepository
	{
		private readonly TravelBuddiesDbContext _context;

        public VehicleRepository(TravelBuddiesDbContext context)
        {
            _context = context;
        }

		public async Task CreateVehicleAsync(Vehicle vehicle)
		{
			await _context.AddAsync(vehicle);
		}

		public void DeleteVehicle(Vehicle vehicle)
		{
			_context.Remove(vehicle);
		}

		public async Task<Vehicle?> GetVehicleById(int vehicleId)
		{
			return await _context
				.Vehicles
				.FirstOrDefaultAsync(v => v.Id == vehicleId);
		}

		public void UpdateVehicleAsync(Vehicle vehicle)
		{
			_context.Update(vehicle);
		}
	}
}
