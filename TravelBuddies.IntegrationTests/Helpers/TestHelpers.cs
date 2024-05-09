namespace TravelBuddies.IntegrationTests.Helpers
{
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using TravelBuddies.Application;

	public class TestHelpers
	{

		public static IMediator CreateMediator()
		{
			var services = new ServiceCollection();
			services.AddMediatR(m => m.RegisterServicesFromAssemblies(typeof(BaseHandler).Assembly));

			var serviceProvider = services.BuildServiceProvider();
			return serviceProvider.GetRequiredService<IMediator>();
		}
	}
}
