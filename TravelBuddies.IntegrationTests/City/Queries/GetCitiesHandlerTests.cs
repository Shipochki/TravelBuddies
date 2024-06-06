namespace TravelBuddies.IntegrationTests.City.Queries
{
	using TravelBuddies.Application.City.Queries.GetCities;
	using TravelBuddies.Domain.Entities;

	public class GetCitiesHandlerTests : BaseHandlerTests
	{

		[Fact]
        public async Task GetCities_ShouldReturnAllCities()
        {
            //Arrange
            var handler = new GetCitiesHandler(_repostiory, _userManager, _roleManager);
            var query = new GetCitiesQuery();

            var country = new Country()
            {
                Name = "testcountry",
                CreatedOn = DateTime.Now 
            };

            var cities = new List<City>()
            {
                new City()
                {
                    Name = "test1",
                    Country = country,
                    CountryId = country.Id,
                    CreatedOn = DateTime.Now
                },
                new City()
                {
                    Name = "test2",
                    Country = country,
                    CountryId = country.Id,
                    CreatedOn = DateTime.Now
                },
                new City()
                {
                    Name = "test3",
                    Country = country,
                    CountryId = country.Id,
                    CreatedOn = DateTime.Now
                },
            };

            await _dbContext.AddRangeAsync(cities);
            await _dbContext.SaveChangesAsync();

            //Act
            var actualResult = await handler.Handle(query, default);

            //Assert
            Assert.Equal(cities.Count, actualResult.Count);

            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(cities[i].Name, actualResult[i].Name);
                Assert.Equal(cities[i].Id, actualResult[i].Id);
                Assert.Equal(cities[i].Country.Id, actualResult[i].CountryId);
                Assert.Equal(cities[i].Country.Id, actualResult[i].CountryId);
                Assert.Equal(cities[i].CreatedOn.Date, actualResult[i].CreatedOn.Date);
            }
        }
    }
}
