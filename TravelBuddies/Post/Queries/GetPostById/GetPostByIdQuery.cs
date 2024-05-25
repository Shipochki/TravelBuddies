namespace TravelBuddies.Application.Post.Queries.GetPostById
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class GetPostByIdQuery : IRequest<Post>
	{
		public int PostId { get; set; }
	}
}
