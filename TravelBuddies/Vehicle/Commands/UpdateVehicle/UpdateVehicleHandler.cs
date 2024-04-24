namespace TravelBuddies.Application.Vehicle.Commands.UpdateVehicle
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

	public class UpdateVehicleHandler : BaseHandler, IRequestHandler<UpdateVehicleCommand, Task>
	{
		public UpdateVehicleHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Task> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
		{
			Vehicle? vehicle = await _repository.GetByIdAsync<Vehicle>(request.Id);

			if (vehicle == null)
			{
				throw new VehicleNotFoundException(
					string.Format(VehicleNotFoundMessage, request.Id));
			}

			if(vehicle.OwnerId != request.OwnerId)
			{
				throw new ApplicationUserNotCreatorException(
					string.Format(ApplicationUserNotCreatorMessage, request.OwnerId));
			}

			vehicle.BrandName = request.BrandName;
			vehicle.ModelName = request.ModelName;
			vehicle.Fuel = (Fuel)request.Fuel;
			vehicle.SeatCount = request.SeatCount;
			vehicle.PictureLink = request.PictureLink;
			vehicle.ACSystem = request.ACSystem;

			_repository.Update(vehicle);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
