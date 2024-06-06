namespace TravelBuddies.Application.User.Queries.GetUserById
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Exceptions.NotFound;

	public class GetUserByIdHandler : BaseHandler, IRequestHandler<GetUserByIdQuery, ApplicationUser>
	{
		public GetUserByIdHandler(IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<ApplicationUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			ApplicationUser? applicationUser = await _userManager
				.FindByIdAsync(request.UserId);

			if (applicationUser == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.UserId));
			}

			return await Task.FromResult(applicationUser);
		}
	}
}
