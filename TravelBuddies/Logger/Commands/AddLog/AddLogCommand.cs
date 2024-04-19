namespace TravelBuddies.Application.Logger.Commands.AddLog
{
    using MediatR;

	public class AddLogCommand : IRequest<Task>
	{
        public AddLogCommand(string message)
        {
            Message = message;
        }

        public required string Message { get; set; }
    }
}
