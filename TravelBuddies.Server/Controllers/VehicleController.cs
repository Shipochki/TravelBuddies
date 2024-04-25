namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Domain.Common;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Vehicle.Commands.CreateVehicle;
	using TravelBuddies.Application.Vehicle.Commands.DeleteVehicle;
	using TravelBuddies.Application.Vehicle.Commands.UpdateVehicle;
	using TravelBuddies.Application.Vehicle.Queries.GetVehicleById;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.DTOs.Vehicle;

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
		public async Task<IActionResult> Create([FromBody]CreateVehicleDto createVehicleDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LogLevel logLevel = LogLevel.Error;

			try
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

				logLevel = LogLevel.Information;
				string message = "Succesfully created vehicle";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Created();
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.DriverAndAdmin)]
		public async Task<IActionResult> Delete(int vehicleId)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				await _mediator.Send(new DeleteVehicleCommand(vehicleId, User.Id()));

				logLevel = LogLevel.Information;
				string message = "Succesfully deleted vehhicle";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (VehicleNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserNotCreatorException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return Forbid(m.Message);
			}
			catch (ApplicationUserNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			
		}

		[HttpPost]
		[Route("[action]")]
		[Authorize(Policy = ApplicationPolicies.OnlyDriver)]
		public async Task<IActionResult> Update([FromBody]UpdateVehicleDto updateVehicleDto)
		{
			LogLevel logLevel = LogLevel.Error;

			try
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

				logLevel = LogLevel.Information;
				 string message = "Succesfully updated vehicle";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(message);
			}
			catch (VehicleNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
			catch (ApplicationUserNotCreatorException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return Forbid(m.Message);
			}
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetVehicleById(int vehicleId)
		{
			LogLevel logLevel = LogLevel.Error;

			try
			{
				Vehicle vehicle = await _mediator.Send(new GetVehicleByIdQuery(vehicleId));

				logLevel = LogLevel.Information;
				string message = "Succesfully get vehicle";

				await _fileLogger.LogAsync(logLevel, message);
				await _databaseLogger.LogAsync(logLevel, message);

				return Ok(GetVehicleByIdDto.FromVehicle(vehicle));
			}
			catch (VehicleNotFoundException m)
			{
				await _fileLogger.LogAsync(logLevel, m.Message);
				await _databaseLogger.LogAsync(logLevel, m.Message);

				return NotFound(m.Message);
			}
		}
	}
}
