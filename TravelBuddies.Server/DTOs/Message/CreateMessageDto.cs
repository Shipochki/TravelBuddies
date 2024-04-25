﻿namespace TravelBuddies.Presentation.DTOs.Message
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Common.DataConstants.MessageConstants;

    public class CreateMessageDto
	{
		[Required]
		[MinLength(MinLengthText)]
		[MaxLength(MaxLengthText)]
		public required string Text { get; set; }

		public int GroupId { get; set; }
	}
}
