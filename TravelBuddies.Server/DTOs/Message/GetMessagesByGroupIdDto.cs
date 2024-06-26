﻿namespace TravelBuddies.Presentation.DTOs.Message
{
	using TravelBuddies.Application.Common.Extensions;
	using TravelBuddies.Domain.Entities;

	public class GetMessagesByGroupIdDto
	{
		public int Id { get; set; }

		public required string Text { get; set; }

		public required string CreatorId { get; set; }

		public required string CreatorName { get; set; }

		public int GroupId { get; set; }

		public string? CreatorProfileLink { get; set; }

		public required string CreatedOn { get; set; } 

		public static GetMessagesByGroupIdDto FromMessage(Message message)
		{
			return new GetMessagesByGroupIdDto()
			{
				Id = message.Id,
				Text = message.Text,
				CreatorId = message.CreatorId,
				CreatorName = $"{message.Creator.FirstName} {message.Creator.LastName}",
				GroupId = message.GroupId,
				CreatorProfileLink = message.Creator.ProfilePictureLink,
				CreatedOn = $"{message.CreatedOn.TimeAgo()}"
			};
		}
	}
}
