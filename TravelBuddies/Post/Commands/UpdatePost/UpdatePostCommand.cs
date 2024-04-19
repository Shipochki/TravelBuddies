namespace TravelBuddies.Application.Post.Commands.UpdatePost
{
	using MediatR;

	public class UpdatePostCommand : PostDto, IRequest<Task>
	{
	}
}
