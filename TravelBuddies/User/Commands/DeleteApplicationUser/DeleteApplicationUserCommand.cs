namespace TravelBuddies.Application.User.Commands.DeleteApplicationUser
{
	using MediatR;

	public class DeleteApplicationUserCommand : IRequest<Task>
	{
        public DeleteApplicationUserCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
	}
}
