﻿namespace TravelBuddies.Domain.EntityModels
{
	using System.ComponentModel.DataAnnotations;

	public abstract class BaseEntity<T> : IActionInfo
	{
		[Key]
		public T? Id { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }
	}
}
