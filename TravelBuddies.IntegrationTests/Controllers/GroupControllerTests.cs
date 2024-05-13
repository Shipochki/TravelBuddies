namespace TravelBuddies.IntegrationTests.Controllers
{
	using Microsoft.AspNetCore.Http;
	using NSubstitute;
	using NSubstitute.ExceptionExtensions;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Security.Principal;
	using TravelBuddies.Application.Exceptions;
	using TravelBuddies.Application.Group.Queries.GetUserGroupsByUserId;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Presentation.Controllers;

	public class GroupControllerTests : BaseControllerTests
	{
        private readonly GroupController _controller;
        public GroupControllerTests()
        {
            _controller = new GroupController(_mediator);
        }

        [Fact]
        public async Task GetUserGroupsByUserId_WithNonValidUser_ShouldCatchException()
        {
            _mediator
				.Send(Arg.Any<GetUserGroupsByUserIdQuery>(), Arg.Any<CancellationToken>())
				.Returns<List<Group>>(_ => throw new ApplicationUserNotFoundException(string.Format("")));

			var user = new ApplicationUser() { UserName = "test", Email = "email" };

			await _dbContext.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(JwtRegisteredClaimNames.NameId, user.Id),
				new Claim("name", user.UserName),
			};

			var identity = new ClaimsIdentity(claims, "Test");
			var claimsPrincipal = new ClaimsPrincipal(identity);

			var principal = Substitute.For<IPrincipal>();
			principal.Identity.Returns(identity);
			principal.IsInRole(Arg.Any<string>()).Returns(true);

			var httpContext = Substitute.For<HttpContext>();
			httpContext.User.Returns(claimsPrincipal);

			var actionResult = await _controller.GetUserGroupsByUserId();
		}
	}
}
