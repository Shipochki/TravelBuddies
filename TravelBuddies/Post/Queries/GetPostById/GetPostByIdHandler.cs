namespace TravelBuddies.Application.Post.Queries.GetPostById
{
	using MediatR;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Common.Exceptions.NotFound;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Domain.Entities;
	using static TravelBuddies.Application.Common.Exceptions.Messages.ExceptionMessages;

	public class GetPostByIdHandler : BaseHandler, IRequestHandler<GetPostByIdQuery, Post>
	{
		public GetPostByIdHandler(
			IRepository repository
			, UserManager<ApplicationUser> userManager
			, RoleManager<IdentityRole> roleManager)
			: base(repository, userManager, roleManager)
		{
		}

		public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
		{
			Post? post = await _repository
				.All<Post>(
					p => p.Id == request.PostId
					&& p.IsDeleted == false
					&& p.IsCompleted == false)
				.Include(p => p.FromDestinationCity)
				.Include(p => p.ToDestinationCity)
				.FirstOrDefaultAsync();

			if(post == null)
			{
				throw new PostNotFoundException(
					string.Format(PostNotFoundMessage, request.PostId));
			}

			return await Task.FromResult(post);
		}
	}
}
