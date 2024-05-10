namespace TravelBuddies.UnitTests.City.Queries
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

            var expectedResult = _dbContext.Cities.ToList();

            //Act
            var actualResult = await handler.Handle(query, default);

            //Assert
            Assert.Equal(expectedResult.Count, actualResult.Count);

            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Name, actualResult[i].Name);
                Assert.Equal(expectedResult[i].Id, actualResult[i].Id);
                Assert.Equal(expectedResult[i].CountryId, actualResult[i].CountryId);
                Assert.Equal(expectedResult[i].Country, actualResult[i].Country);
                Assert.Equal(expectedResult[i].Country.Name, actualResult[i].Country.Name);
                Assert.Equal(expectedResult[i].Country.Id, actualResult[i].Country.Id);
            }
        }
    }
}
