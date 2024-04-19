namespace TravelBuddies.Application.Logger.Commands.CreateLog
{
    using MediatR;

	public record CreateLogCommand : IRequest<Task>
	{
        public CreateLogCommand(string message)
        {
            Message = message;
        }

        public required string Message { get; set; }
    }
}
