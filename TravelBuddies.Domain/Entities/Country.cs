using TravelBuddies.Domain.Constants;

namespace TravelBuddies.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using TravelBuddies.Domain.EntityModels;
    using static DataConstants.Country;

    public class Country : BaseSoftDeleteEntity<int>
	{
		[Required]
		[MaxLength(MaxLengthCountryName)]
		public required string Name { get; set; }
	}
}
