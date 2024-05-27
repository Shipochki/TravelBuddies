namespace TravelBuddies.Application.User.Commands.UpdateProfilePicture
{
	using MediatR;
	using Microsoft.AspNetCore.Http;

	public class UpdateProfilePictureCommand : IRequest<Task>
	{
		public required string UserId { get; set;}

		public required IFormFile ProfilePicture { get; set;}
	}
}
