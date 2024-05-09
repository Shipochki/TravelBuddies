namespace TravelBuddies.UnitTests.City.Queries
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using NSubstitute;
	using TravelBuddies.Application.City.Queries.GetCities;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure;
	using TravelBuddies.Infrastructure.Repository;

	public class GetCitiesHandlerTests
	{
        private readonly Repository _repostiory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetCitiesHandlerTests()
        {
			var options = new DbContextOptionsBuilder<TravelBuddiesDbContext>()
				.UseInMemoryDatabase(databaseName: "dbName")
				.Options;

			_repostiory = new Repository(new TravelBuddiesDbContext(options));
			_userManager = Substitute.For<UserManager<ApplicationUser>>(
		    Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
			_roleManager = Substitute.For<RoleManager<IdentityRole>>(
			Substitute.For<IRoleStore<IdentityRole>>(), null, null, null, null);
		}

        [Fact]
        public async Task GetCities_ShouldReturnAllCities()
        {
            //Arrange
            var handler = new GetCitiesHandler(_repostiory, _userManager, _roleManager);
            var query = new GetCitiesQuery();

            Country country = new Country()
            {
                Id = 1,
                Name = "Bulgaria",
            };

            var expectedResult = new List<City>()
            {
                new City() { Id = 1, CountryId = country.Id, Country = country, Name = "Sofia"},
                new City() { Id = 2, CountryId = country.Id, Country = country, Name = "Targovishte"},
                new City() { Id = 3, CountryId = country.Id, Country = country, Name = "Pernik"},
            };

            await _repostiory.AddRangeAsync(expectedResult);
            await _repostiory.SaveChangesAsync();

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
