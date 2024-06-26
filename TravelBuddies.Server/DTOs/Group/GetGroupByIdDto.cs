﻿namespace TravelBuddies.Presentation.DTOs.Group
{
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Presentation.DTOs.Message;
	using TravelBuddies.Presentation.DTOs.User;

	public class GetGroupByIdDto
	{
		public int Id { get; set; }

		public required string Name { get; set; }

		public required string Date { get; set; }

		public required UserDto Creator { get; set; }

		public List<UserDto> Members { get; set; } = null!;

		public List<GetMessagesByGroupIdDto> Messages { get; set; } = null!;

		public static GetGroupByIdDto FromGroup(Group group)
		{
			return new GetGroupByIdDto()
			{
				Id = group.Id,
				Name = $"{group.Creator.FirstName} {group.Creator.LastName}",
				Date = group.Post.DateAndTime.ToString("MM.dd.yyyy hh:mm tt"),
				Creator = UserDto.FromUser(group.Creator),
				Members = group.UsersGroups
					.Select(u => UserDto.FromUser(u.User))
					.ToList(),
				Messages = group.Messages
					.Select(GetMessagesByGroupIdDto.FromMessage)
					.Reverse()
					.ToList(),
			};
		}
	}
}
