namespace TravelBuddies.UnitTests.Helpers
{
	using TravelBuddies.Application.Interfaces.Stripe;
	using TravelBuddies.Domain.Entities;

	public class StripeServiceFake : IStripeService
	{
		public string CreateProduct(Post post)
		{
			return "testLink";
		}
	}
}
