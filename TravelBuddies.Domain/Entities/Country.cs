namespace TravelBuddies.Domain.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using TravelBuddies.Domain.EntityModels;
	using static DataConstants.Country;

	public class Country : BaseEntity<int>, ISoftDeleteEntity
	{
		[Required]
		[MaxLength(MaxLengthCountryName)]
		public required string Name { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
