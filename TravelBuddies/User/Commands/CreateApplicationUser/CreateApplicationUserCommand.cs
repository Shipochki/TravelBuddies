namespace TravelBuddies.Application.User.Commands.CreateApplicationUser
{
	using MediatR;

	public class CreateApplicationUserCommand : ApplicationUserDto, IRequest<Task>
	{
	}
}
