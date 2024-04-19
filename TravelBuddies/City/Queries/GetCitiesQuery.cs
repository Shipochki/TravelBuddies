namespace TravelBuddies.Application.City.Queries
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public record GetCitiesQuery : IRequest<IEnumerable<City>>;
}
