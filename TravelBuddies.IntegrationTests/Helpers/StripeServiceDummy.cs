namespace TravelBuddies.IntegrationTests.Helpers
{
	using TravelBuddies.Application.Interfaces.Stripe;
	using TravelBuddies.Domain.Entities;

	public class StripeServiceDummy : IStripeService
	{
		public string CreateProduct(Post post)
		{
			return "testLink";
		}
	}
}
