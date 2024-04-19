namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using TravelBuddies.Domain.Enums;
	using TravelBuddies.Domain.Models;
	using static DataConstants.VehicleConstants;

	public class Vehicle : BaseEntity<int>
	{
		[Required]
		[MaxLength(MaxLengthBrandName)]
		public required string BrandName { get; set; }

		[Required]
		[MaxLength(MaxLengthModelName)]
		public required string ModelName { get; set; }

		public Fuel Fuel { get; set; }

		public int SeatCount { get; set; }

		public string? PictureLink { get; set; }

		public bool ACSystem { get; set; }

		[Required]
		[ForeignKey(nameof(Owner))]
		public required string OwnerId { get; set; }
		public required ApplicationUser Owner { get; set; }
	}
}
