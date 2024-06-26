﻿namespace TravelBuddies.IntegrationTests.Post.Commands
{
    using NSubstitute;
    using TravelBuddies.Application.Common.Exceptions.NotFound;
    using TravelBuddies.Application.Common.Interfaces.Stripe;
    using TravelBuddies.Application.Post.Commands.CreatePost;
    using TravelBuddies.Domain.Entities;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.IntegrationTests.Helpers;

    public class CreatePostHandlerTests : BaseHandlerTests
	{
		private readonly IStripeService _stripeService;

        public CreatePostHandlerTests()
        {
			_stripeService = new StripeServiceDummy();
        }

        [Fact]
		public void CreatePost_WithNonExistingCreator_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreatePostHandler(_repostiory, _userManager, _roleManager, _stripeService);
			var command = new CreatePostCommand()
			{
				CreatorId = "1",
				DateAndTime = "",
				Description = "Description",
				Currency = "Eur"
			};

			//Assert
			Assert.ThrowsAsync<ApplicationUserNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreatePost_WithNonExistingFromDestination_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreatePostHandler(_repostiory, _userManager, _roleManager, _stripeService);

			var user = new ApplicationUser() { UserName = "test", Email = "test" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new CreatePostCommand()
			{
				CreatorId = user.Id,
				DateAndTime = "",
				Description = "Description",
				Currency = "Eur"
			};

			//Assert
			await Assert.ThrowsAsync<CityNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreatePost_WithNonExistingToDestination_ShouldThrowsException()
		{
			//Arrange
			var handler = new CreatePostHandler(_repostiory, _userManager, _roleManager, _stripeService);

			var user = new ApplicationUser() { UserName = "test", Email = "test" };
			var country = new Country() { Name = "bg" };
			var city1 = new City() { Country = country, Name = "city1" };

			await _dbContext.AddAsync(city1);
			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var command = new CreatePostCommand()
			{
				CreatorId = user.Id,
				DateAndTime = "",
				Description = "Description",
				FromDestinationCityId = city1.Id,
				Currency = "eur"
			};

			//Assert
			await Assert.ThrowsAsync<CityNotFoundException>(async () =>
			{
				await handler.Handle(command, default);
			});
		}

		[Fact]
		public async Task CreatePost_WithValidData_ShouldCreatePost()
		{
			//Arrange
			var handler = new CreatePostHandler(_repostiory, _userManager, _roleManager, _stripeService);

			var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
			var country = new Country() { Name = "country" };
			var city1 = new City() { Country = country, Name = "Sofia" };
			var city2 = new City() { Country = country, Name = "Targovishte" };

			await _dbContext.AddAsync(user);
			await _dbContext.AddRangeAsync(city1, city2);
			await _dbContext.SaveChangesAsync();

			var command = new CreatePostCommand()
			{
				CreatorId = user.Id,
				DateAndTime = DateTime.Now.ToString(),
				Description = "Description",
				Currency = "eur",
				FromDestinationCityId = city1.Id,
				ToDestinationCityId = city2.Id,
				PaymentType = (int)PaymentType.CashAndCard,
				PricePerSeat = 5,
				Pets = true,
			};

			//Act
			var result = await handler.Handle(command, default);

			//Assert
			Assert.Equal(result.CreatorId, user.Id);
			Assert.Equal(result.FromDestinationCityId, city1.Id);
			Assert.Equal(result.FromDestinationCity, city1);
			Assert.Equal(result.ToDestinationCityId, city2.Id);
			Assert.Equal(result.ToDestinationCity, city2);
			Assert.Equal(result.PricePerSeat, command.PricePerSeat);
			Assert.False(result.Baggage);
			Assert.True(result.Pets);
			Assert.Equal(result.DateAndTime.Date, DateTime.Now.Date);
			Assert.Equal("testLink", result.PaymentLink);
			Assert.Equal(result.Creator, user);
			Assert.Equal(result.CreatorId, user.Id);
		}
	}
}
