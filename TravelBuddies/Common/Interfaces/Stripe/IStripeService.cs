namespace TravelBuddies.Application.Common.Interfaces.Stripe
{
    using TravelBuddies.Domain.Entities;

    public interface IStripeService
    {
        public string CreateProduct(Post post);
    }
}
