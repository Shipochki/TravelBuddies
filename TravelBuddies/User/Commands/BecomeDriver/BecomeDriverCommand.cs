namespace TravelBuddies.Application.User.Commands.BecomeDriver
{
    using MediatR;

	public class BecomeDriverCommand : IRequest<Task>
	{
        public BecomeDriverCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
	}
}
