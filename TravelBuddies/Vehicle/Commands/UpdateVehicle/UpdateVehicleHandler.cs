﻿namespace TravelBuddies.Application.Vehicle.Commands.UpdateVehicle
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Interfaces.AzureStorage;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using static TravelBuddies.Application.Exceptions.Messages.ExceptionMessages;

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
			
			vehicle.ACSystem = request.ACSystem;

			_repository.Update(vehicle);
			await _repository.SaveChangesAsync();

			return Task.CompletedTask;
		}
	}
}
