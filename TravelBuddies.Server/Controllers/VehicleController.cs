namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Domain.Common;
	using TravelBuddies.Application.Vehicle.Commands.CreateVehicle;
	using TravelBuddies.Application.Vehicle.Commands.DeleteVehicle;
	using TravelBuddies.Application.Vehicle.Commands.UpdateVehicle;
	using TravelBuddies.Application.Vehicle.Queries.GetVehicleById;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.DTOs.Vehicle;
	using TravelBuddies.Application.Vehicle.Queries.GetVehicleByOwnerId;

	[EnableCors(ApplicationCorses.AllowOrigin)]
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class VehicleController : BaseController
	{
		public VehicleController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		//[ModelStateValidation]
		public async Task<IActionResult> Create([FromForm] CreateVehicleDto createVehicleDto)
		{
			CreateVehicleCommand command = new CreateVehicleCommand()
			{
				BrandName = createVehicleDto.BrandName,
				ModelName = createVehicleDto.ModelName,
				Fuel = createVehicleDto.Fuel,
				SeatCount = createVehicleDto.SeatCount,
				PictureLink = createVehicleDto.PictureLink,
				ACSystem = createVehicleDto.ACSystem,
				OwnerId = User.Id(),
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully created vehicle";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Created();
		}

		[HttpPost]
		[Route("[action]/{vehicleId}")]
		[Authorize(Policy = ApplicationPolicies.DriverAndAdmin)]
		public async Task<IActionResult> Delete(int vehicleId)
		{
			await _mediator.Send(new DeleteVehicleCommand(vehicleId, User.Id()));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully deleted vehhicle";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(message);

		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		//[ModelStateValidation]
		public async Task<IActionResult> Update([FromForm] UpdateVehicleDto updateVehicleDto)
		{
			UpdateVehicleCommand command = new UpdateVehicleCommand()
			{
				Id = updateVehicleDto.Id,
				BrandName = updateVehicleDto.BrandName,
				ModelName = updateVehicleDto.ModelName,
				Fuel = updateVehicleDto.Fuel,
				SeatCount = updateVehicleDto.SeatCount,
				ACSystem = updateVehicleDto.ACSystem,
				OwnerId = User.Id(),
				PictureLink = updateVehicleDto.PictureLink,
			};

			await _mediator.Send(command);

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully updated vehicle";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok();
		}

		[HttpGet]
		[Route("[action]/{vehicleId}")]
		public async Task<IActionResult> GetVehicleById(int vehicleId)
		{
			Vehicle vehicle = await _mediator.Send(new GetVehicleByIdQuery(vehicleId));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully get vehicle";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			return Ok(VehicleDto.FromVehicle(vehicle));
		}

		[HttpGet]
		[Route("[action]/{ownerId}")]
		public async Task<IActionResult> GetVehicleByOwnerId(string ownerId)
		{
			Vehicle? vehicle = await _mediator.Send(new GetVehicleByOwnerIdQuery(ownerId));

			LogLevel logLevel = LogLevel.Information;
			string message = "Succesfully get vehicle by onwer id";

			await _fileLogger.LogAsync(logLevel, message);
			await _databaseLogger.LogAsync(logLevel, message);

			if(vehicle == null)
			{
				return Ok();
			}

			return Ok(VehicleDto.FromVehicle(vehicle));
		}
	}
}
