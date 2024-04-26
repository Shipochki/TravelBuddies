namespace TravelBuddies.Application.Group.Queries.GetUserGroupsByUserId
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class GetUserGroupsByUserIdQuery : IRequest<List<Group>>
	{
        public GetUserGroupsByUserIdQuery(string userId)
        {
			UserId = userId;
        }

		public string UserId { get; set; }
	}
}
