namespace TravelBuddies.Application.Interfaces.Stripe
{
	using TravelBuddies.Domain.Entities;

	public interface IStripeService
	{
		public string CreateProduct(Post post);
	}
}
