namespace TravelBuddies.Application.Message.Queries.GetMessagesByGroupId
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class GetMessagesByGroupIdQuery : IRequest<IEnumerable<Message>>
	{
        public GetMessagesByGroupIdQuery(int groupId, string userId)
        {
            GroupId = groupId;
			UserId = userId;
        }

        public int GroupId { get; set; }

		public required string UserId { get; set; }
	}
}
