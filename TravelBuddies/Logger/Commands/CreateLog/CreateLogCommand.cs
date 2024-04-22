namespace TravelBuddies.Application.Logger.Commands.CreateLog
{
    using MediatR;
	using TravelBuddies.Domain.Enums;

	public class CreateLogCommand : IRequest<Task>
	{
        public CreateLogCommand(string message, LogLevel level)
        {
            Message = message;
            Level = level;
        }

        public string Message { get; set; } 

        public LogLevel Level { get; set; } 
    }
}
