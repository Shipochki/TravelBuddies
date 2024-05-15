namespace TravelBuddies.Application.Vehicle.Queries.GetVehicleByOwnerId
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Repository;
    using TravelBuddies.Domain.Entities;

    public class GetVehicleByOwnerIdHandler : BaseHandler, IRequestHandler<GetVehicleByOwnerIdQuery, Vehicle?>
	{
		public GetVehicleByOwnerIdHandler(IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Vehicle?> Handle(GetVehicleByOwnerIdQuery request, CancellationToken cancellationToken)
		{
			Vehicle? vehicle = await _repository
				.FirstOrDefaultAsync<Vehicle>(v => v.OwnerId == request.OwnerId);

			return await Task.FromResult(vehicle);
		}
	}
}
