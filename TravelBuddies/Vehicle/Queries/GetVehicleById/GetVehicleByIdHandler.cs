namespace TravelBuddies.Application.Vehicle.Queries.GetVehicleById
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Exceptions.ExceptionMessages;

	public class GetVehicleByIdHandler : BaseHandler, IRequestHandler<GetVehicleByIdQuery, Vehicle>
	{
		public GetVehicleByIdHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Vehicle> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
		{
			Vehicle? vehicle = await _repository.GetByIdAsync<Vehicle>(request.VehicleId);

			if (vehicle == null)
			{
				throw new VehicleNotFoundException(
					string.Format(VehicleNotFoundMessage, request.VehicleId));
			}

			return await Task.FromResult(vehicle);
		}
	}
}
