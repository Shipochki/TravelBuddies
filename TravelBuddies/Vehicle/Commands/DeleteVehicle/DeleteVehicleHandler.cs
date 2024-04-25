namespace TravelBuddies.Application.Vehicle.Commands.DeleteVehicle
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using TravelBuddies.Domain.Constants;
    using TravelBuddies.Application.Exceptions;
    using TravelBuddies.Application.Repository;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

    public class DeleteVehicleHandler : BaseHandler, IRequestHandler<DeleteVehicleCommand, Task>
	{
		public DeleteVehicleHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
		{
			Vehicle? vehicle = await _repository.GetByIdAsync<Vehicle>(request.VehicleId);

			if (vehicle == null)
			{
				throw new VehicleNotFoundException(
					string.Format(VehicleNotFoundMessage, request.VehicleId));
			}

			ApplicationUser? user = await _userManager.FindByIdAsync(request.OwnerId);

			if (user == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.OwnerId));
			}

			if (vehicle.OwnerId != request.OwnerId 
				&& !await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.OwnerId));
			}

			_repository.Delete(vehicle);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
