namespace TravelBuddies.Presentation.DTOs.Post
{
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Presentation.DTOs.User;

	public class GetAllPostsBySearchDto
	{
		public int Id { get; set; }

		public required string FromDestinationName { get; set; }

		public required string ToDestinationName { get; set; }

		public required string Description { get; set; }

		public decimal PricePerSeat { get; set; }

		public required string Currency {  get; set; }

		public int FreeSeats { get; set; }

		public bool Baggage { get; set; }

		public bool Pets { get; set; }

		public required string DateAndTime { get; set; }

		public int? GroupId { get; set; }

		public required UserDto Creator { get; set; }

		public List<string> Participants { get; set; } = null!;

		public List<string> BlackListsUsers { get; set; } = null!;

		public static GetAllPostsBySearchDto FromPost(Post post)
		{
			return new GetAllPostsBySearchDto()
			{
				Id = post.Id,
				FromDestinationName = post.FromDestinationCity.Name,
				ToDestinationName = post.ToDestinationCity.Name,
				Description = post.Description,
				PricePerSeat = post.PricePerSeat,
				Currency = post.Currency,
				Baggage = post.Baggage,
				Pets = post.Pets,
				DateAndTime = post.DateAndTime.ToString("yyyy-MM-dd   hh:mm tt"),
				GroupId = post.GroupId,
				Creator = UserDto.FromUser(post.Creator),
				Participants = post.Group.UsersGroups.Select(ug => ug.UserId).ToList(),
				BlackListsUsers = post.Group.UsersBlackLists.Select(ug => ug.UserId).ToList(),
				FreeSeats = post.FreeSeats - post.Group.UsersGroups.Select(ug => ug.UserId).ToList().Count() + 1,
			};
		}
	}
}
