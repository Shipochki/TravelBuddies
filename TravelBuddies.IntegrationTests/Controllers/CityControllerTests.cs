namespace TravelBuddies.IntegrationTests.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using NSubstitute;
	using System.Net;
	using TravelBuddies.Application.City.Queries.GetCities;
	using TravelBuddies.Domain.Entities;
    using TravelBuddies.Presentation.Controllers;

	public class CityControllerTests : BaseControllerTests
	{
        private readonly CityController _controller;

        public CityControllerTests()
        {
            _controller = new CityController(_mediator);
        }

        [Fact]
        public async void GetCities_ShouldReturnCities()
        {
            var country = new Country() { Name = "Country" };
            var cities = new List<City>()
            {
                new City() { Name = "city1", Country = country },
                new City() { Name = "city2", Country = country },
            };

            _mediator.Send(Arg.Any<GetCitiesQuery>(), Arg.Any<CancellationToken>())
                .Returns(cities);

            var actionResult = await _controller.GetCities();

            var result = actionResult as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }
    }
}
