namespace TravelBuddies.IntegrationTests.Helpers
{
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using TravelBuddies.Application;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Infrastructure.Repository;

	public class TestHelpers
	{
		public static IMediator CreateMediator(IRepository repository)
		{
			var services = new ServiceCollection();
			services.AddMediatR(m => m.RegisterServicesFromAssemblies(typeof(BaseHandler).Assembly));
			services.AddScoped<IRepository, Repository>();

			var serviceProvider = services.BuildServiceProvider();
			return serviceProvider.GetRequiredService<IMediator>();
		}
	}
}
