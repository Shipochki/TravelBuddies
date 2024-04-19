namespace TravelBuddies.Application.Post.Commands.DeletePost
{
	using MediatR;

	public class DeletePostCommand : IRequest<Task>
	{
        public DeletePostCommand(int postId, string creatorId)
        {
            PostId = postId;
			CreatorId = creatorId;
        }

        public int PostId { get; set; }

		public string CreatorId { get; set; }
	}
}
