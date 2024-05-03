namespace TravelBuddies.Application.Group.Queries.GetGroupById
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class GetGroupByIdQuery : IRequest<Group>
	{
		public GetGroupByIdQuery(int groupId, string userId) 
		{
			GroupId = groupId;
			UserId = userId;
		}

        public int GroupId { get; set; }

		public string UserId { get; set; }
    }
}
