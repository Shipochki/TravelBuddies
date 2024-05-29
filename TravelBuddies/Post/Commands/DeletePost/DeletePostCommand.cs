namespace TravelBuddies.Application.Post.Commands.DeletePost
{
	using MediatR;

	public record DeletePostCommand : IRequest<int>
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
