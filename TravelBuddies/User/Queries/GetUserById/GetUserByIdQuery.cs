namespace TravelBuddies.Application.User.Queries.GetUserById
{
	using MediatR;
	using TravelBuddies.Domain.Entities;

	public class GetUserByIdQuery : IRequest<ApplicationUser>
	{
		public GetUserByIdQuery(string userId) 
		{
			UserId = userId;
		}

		public string UserId { get; set; }
	}
}
