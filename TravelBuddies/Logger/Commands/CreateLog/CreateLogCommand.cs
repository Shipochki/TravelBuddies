namespace TravelBuddies.Application.Logger.Commands.CreateLog
{
    using MediatR;

	public class CreateLogCommand : IRequest<Task>
	{
        public CreateLogCommand(string message)
        {
            Message = message;
        }

        public string Message { get; set; } 
    }
}
