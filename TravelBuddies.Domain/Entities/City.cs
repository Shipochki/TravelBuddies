namespace TravelBuddies.Domain.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using TravelBuddies.Domain.EntityModels;
	using static DataConstants.City;

	public class City : BaseEntity<int>, ISoftDeleteEntity
	{
		[Required]
		[MaxLength(MaxLengthCityName)]
		public required string Name { get; set; }

		[Required]
		[ForeignKey(nameof(Country))]
		public int CountryId { get; set; }
		public required Country Country { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
