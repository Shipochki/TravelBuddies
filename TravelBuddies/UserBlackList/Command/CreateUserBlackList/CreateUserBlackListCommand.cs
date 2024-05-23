namespace TravelBuddies.Application.UserBlackList.Command.CreateUserBlackList
{
	using MediatR;

	public class CreateUserBlackListCommand : UserBlackListDto, IRequest<Task>
	{
	}
}
