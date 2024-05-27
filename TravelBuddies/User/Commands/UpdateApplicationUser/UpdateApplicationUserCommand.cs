namespace TravelBuddies.Application.User.Commands.UpdateApplicationUser
{
	using MediatR;

	public class UpdateApplicationUserCommand : IRequest<Task>
	{
		public required string Id { get; set; }	
		public required string FirstName { get; set; }

		public required string LastName { get; set; }

		public string? City { get; set; }

		public string? Country { get; set; }
	}
}
