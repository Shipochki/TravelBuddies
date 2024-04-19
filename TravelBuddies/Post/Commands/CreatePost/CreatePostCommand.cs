namespace TravelBuddies.Application.Post.Commands.CreatePost
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class CreatePostCommand : PostDto, IRequest<Post>
	{
	}
}
