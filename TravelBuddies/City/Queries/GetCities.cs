namespace TravelBuddies.Application.City.Queries
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public record GetCities : IRequest<IEnumerable<City>>;
}
