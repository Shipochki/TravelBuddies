namespace TravelBuddies.Application.City.Queries.GetCities
{
    using MediatR;
    using TravelBuddies.Domain.Entities;

    public record GetCitiesQuery : IRequest<List<City>>;
}
