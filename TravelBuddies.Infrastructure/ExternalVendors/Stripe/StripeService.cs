﻿namespace TravelBuddies.Infrastructure.ExternalVendors.Stripe
{
	using TravelBuddies.Application.Interfaces.Stripe;
	using TravelBuddies.Domain.Entities;
	using global::Stripe;

	public class StripeService : IStripeService
	{
		public string CreateProduct(Post post)
		{
			var createProductOptions = new ProductCreateOptions
			{
				Id = post.Id.ToString(),
				Name = $"{post.Id} - {post.Creator.Email}",
				DefaultPriceData = new ProductDefaultPriceDataOptions
				{
					UnitAmount = (long)(post.PricePerSeat * 100),
					Currency = "bgn",
				},

			};

			var productService = new ProductService();
			var createdProduct = productService.Create(createProductOptions);
			string price = createdProduct.DefaultPriceId;

			var createLinkOptions = new PaymentLinkCreateOptions
			{
				LineItems = new List<PaymentLinkLineItemOptions>
					{
						new PaymentLinkLineItemOptions { Price = price, Quantity = 1 },
					},
				AfterCompletion = new PaymentLinkAfterCompletionOptions
				{
					Type = "redirect",
					Redirect = new PaymentLinkAfterCompletionRedirectOptions
					{
						Url = $"https://localhost:5173/success/?productId={createdProduct.Id}"
					},
				},

			};

			var linkService = new PaymentLinkService();

			return linkService.Create(createLinkOptions).Url;
		}
	}
}
