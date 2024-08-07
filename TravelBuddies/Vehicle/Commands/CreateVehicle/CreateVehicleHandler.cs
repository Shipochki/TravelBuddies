﻿namespace TravelBuddies.Application.Vehicle.Commands.CreateVehicle
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Interfaces.AzureStorage;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using Microsoft.AspNetCore.Http;

	public class CreateVehicleHandler : BaseHandler, IRequestHandler<CreateVehicleCommand, Vehicle>
	{
		private readonly IBlobService _blobService;
		public CreateVehicleHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager
			, IBlobService blobService)
			: base(repository, userManager, roleManager)
		{
			_blobService = blobService;
		}

		public async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? owner = await _userManager
				.FindByIdAsync(request.OwnerId);

			if (owner == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.OwnerId));
			}

			//string pictureLink = await _blobService.UploadImageAsync(request.PictureLink);
			string pictureLink = string.Empty;

			Vehicle vehicle = new Vehicle()
			{
				BrandName = request.BrandName,
				ModelName = request.ModelName,
				Fuel = (Fuel)request.Fuel,
				SeatCount = request.SeatCount,
				PictureLink = pictureLink,
				ACSystem = request.ACSystem,
				Owner = owner,
				OwnerId = request.OwnerId,
				CreatedOn = DateTime.Now,
				Year = request.Year,
				Color = request.Color
			};

			await _repository.AddAsync(vehicle);
			await _repository.SaveChangesAsync();

			return await Task.FromResult(vehicle);
		}
	}
}
