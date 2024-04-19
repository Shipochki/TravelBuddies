namespace TravelBuddies.Application.City.Queries
{
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetCitiesHandler : BaseHandler, IRequestHandler<GetCities, IEnumerable<City>>
	{
		public GetCitiesHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<IEnumerable<City>> Handle(GetCities request, CancellationToken cancellationToken)
		{
			List<City> cities = await _repository.All<City>().ToListAsync();
			return await Task.FromResult(cities);
		}
	}
}
