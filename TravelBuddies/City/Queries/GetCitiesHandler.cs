namespace TravelBuddies.Application.City.Queries
{
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetCitiesHandler : IRequestHandler<GetCities, IEnumerable<City>>
	{
		private readonly IRepository _repository;

        public GetCitiesHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<City>> Handle(GetCities request, CancellationToken cancellationToken)
		{
			List<City> cities = await _repository.All<City>().ToListAsync();
			return await Task.FromResult(cities);
		}
	}
}
