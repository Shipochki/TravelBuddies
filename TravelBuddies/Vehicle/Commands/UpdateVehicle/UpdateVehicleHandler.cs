namespace TravelBuddies.Application.Vehicle.Commands.UpdateVehicle
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;

    public class UpdateVehicleHandler : BaseHandler, IRequestHandler<UpdateVehicleCommand, Task>
	{
		private readonly IBlobService _blobService;

		public UpdateVehicleHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IBlobService blobService)
			: base(repository, userManager, roleManager)
		{
			_blobService = blobService;
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

			if(request.PictureLink != null)
			{
				string pictureLink = await _blobService.UploadImageAsync(request.PictureLink);
				
				vehicle.PictureLink = pictureLink;
			}

			vehicle.BrandName = request.BrandName;
			vehicle.ModelName = request.ModelName;
			vehicle.Fuel = (Fuel)request.Fuel;
			vehicle.SeatCount = request.SeatCount;
			vehicle.Year = request.Year;
			vehicle.Color = request.Color;
			vehicle.ACSystem = request.ACSystem;

			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
