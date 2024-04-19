namespace TravelBuddies.Application.Group.Commands.CreateGroup
{
	using MediatR;
	using System.Threading;
	using System.Threading.Tasks;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;

	public class CreateGroupHandler : BaseHandler, IRequestHandler<CreateGroupCommand, Group>
	{
		public CreateGroupHandler(IRepository repository) 
			: base(repository)
		{
		}

		public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
		{
			ApplicationUser? creator = await _repository.GetByIdAsync<ApplicationUser>(request.CreatorId);

			if (creator == null)
			{
				throw new ArgumentNullException($"Non-extitent User with Id {request.CreatorId}");
			}

			Post? post = await _repository.GetByIdAsync<Post>(request.PostId);

			if (post == null)
			{
				throw new ArgumentNullException($"Non-extitent Post with Id {request.PostId}");
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
