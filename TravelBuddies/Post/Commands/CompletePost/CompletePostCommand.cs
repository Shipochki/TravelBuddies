namespace TravelBuddies.Application.Post.Commands.CompletePost
{
	using MediatR;

	public record CompletePostCommand : IRequest<Task>
	{
        public CompletePostCommand(int postId, string creatorId)
        {
            PostId = postId;
			CreatorId = creatorId;
        }

        public int PostId { get; set; }

		public string CreatorId { get; set; }
	}
}
