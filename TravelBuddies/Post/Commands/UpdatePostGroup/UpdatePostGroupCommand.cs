namespace TravelBuddies.Application.Post.Commands.UpdatePostGroup
{
	using MediatR;

	public record UpdatePostGroupCommand : IRequest<Task>
	{
        public UpdatePostGroupCommand(int postId, int groupId)
        {
            PostId = postId;
            GroupId = groupId;
        }

        public int PostId { get; set; }
        public int GroupId { get; set; }
	}
}
