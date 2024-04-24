namespace TravelBuddies.Presentation.Controllers
{
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Application.City.Queries;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Presentation.DTOs.City;

	[EnableCors(ApplicationCorses.AllowOrigin)]
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CityController : BaseController
	{
		public CityController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetCities()
		{
			List<City> cities = await _mediator.Send(new GetCitiesQuery());
			return Ok(cities.Select(GetCitiesDto.FromCity));
		}
	}
}
