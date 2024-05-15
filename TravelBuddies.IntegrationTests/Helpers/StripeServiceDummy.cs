namespace TravelBuddies.IntegrationTests.Helpers
{
    using TravelBuddies.Application.Common.Interfaces.Stripe;
    using TravelBuddies.Domain.Entities;

    public class StripeServiceDummy : IStripeService
	{
		public string CreateProduct(Post post)
		{
			return "testLink";
		}
	}
}
