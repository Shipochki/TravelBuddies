namespace TravelBuddies.Application.Group.Commands.CreateGroup
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Exceptions;
    using TravelBuddies.Domain.Entities;
    using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;
    using TravelBuddies.Application.Common.Interfaces.Repository;

    public class CreateGroupHandler : BaseHandler, IRequestHandler<CreateGroupCommand, Group>
	{
		public CreateGroupHandler(IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _userManager.FindByIdAsync(request.CreatorId);

			if (creator == null)
			{
				throw new ApplicationUserNotFoundException(
					string.Format(ApplicationUserNotFoundMessage, request.CreatorId));
			}

			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new PostNotFoundException(
					string.Format(PostNotFoundMessage, request.PostId));
			}

			Group group = new Group()
			{
				Post = post,
				PostId = post.Id,
				Creator = creator,
				CreatorId = creator.Id,
				CreatedOn = DateTime.Now
			};

			await _repository.AddAsync(group);
			await _repository.SaveChangesAsync();
			return await Task.FromResult(group);
		}
	}
}
