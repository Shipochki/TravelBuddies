using TravelBuddies.Domain.Common;

namespace TravelBuddies.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TravelBuddies.Domain.Enums;
    using TravelBuddies.Domain.EntityModels;
    using static DataConstants.VehicleConstants;

    public class Vehicle : BaseEntity<int>
	{
		public required string BrandName { get; set; }

		public required string ModelName { get; set; }

		public Fuel Fuel { get; set; }

		public int SeatCount { get; set; }

		public required string PictureLink { get; set; }

		public bool ACSystem { get; set; }

		public required string OwnerId { get; set; }

		public required ApplicationUser Owner { get; set; }
	}
}
