namespace TravelBuddies.Application.Post.Queries.GetPostsByOwnerId
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class GetPostsByOwnerIdQuery : IRequest<List<Post>>
	{
        public GetPostsByOwnerIdQuery(string ownerId)
        {
            OwnerId = ownerId;
        }

        public string OwnerId { get; set; }
	}
}
