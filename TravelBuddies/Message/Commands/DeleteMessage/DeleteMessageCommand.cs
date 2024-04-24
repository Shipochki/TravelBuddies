namespace TravelBuddies.Application.Message.Commands.DeleteMessage
{
	using MediatR;

	public record DeleteMessageCommand : IRequest<Task>
	{
        public DeleteMessageCommand(int messageId, string creatorId)
        {
            MessageId = messageId;
			CreatorId = creatorId;
        }

        public int MessageId { get; set; }

		public string CreatorId { get; set; }
	}
}
