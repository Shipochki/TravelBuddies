﻿namespace TravelBuddies.Application.City.Queries
{
	using MediatR;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class GetCitiesHandler : BaseHandler, IRequestHandler<GetCitiesQuery, IEnumerable<City>>
	{
		private List<City> _cities;
		private DateTime _loaded;

		public GetCitiesHandler(IRepository repository) 
			: base(repository)
		{
			_cities = new List<City>();
			_loaded = new DateTime();
		}

		public async Task<IEnumerable<City>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
		{
			if (new DateTime(_loaded.Year,
				_loaded.Month,
				_loaded.Day + 1
				) < DateTime.Now)
			{
				_cities = await _repository.All<City>().ToListAsync();
				_loaded = DateTime.Now;
			}

			return await Task.FromResult(_cities);
		}
	}
}
