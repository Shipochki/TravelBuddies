namespace TravelBuddies.IntegrationTests.Controllers
{
    using MediatR;
	using Microsoft.EntityFrameworkCore;
	using NSubstitute;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Infrastructure;
	using TravelBuddies.Infrastructure.Repository;

	public class BaseControllerTests
	{
        protected readonly IMediator _mediator;
        protected readonly TravelBuddiesDbContext _dbContext;
		protected readonly IRepository _repository;
		private readonly DbContextOptions<TravelBuddiesDbContext> _options;

		public BaseControllerTests()
        {
             _options = new DbContextOptionsBuilder<TravelBuddiesDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			_dbContext = new TravelBuddiesDbContext(_options);

			_repository = new Repository(_dbContext);
			
			_mediator = Substitute.For<IMediator>();
		}
    }
}
