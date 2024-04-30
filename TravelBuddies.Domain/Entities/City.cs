namespace TravelBuddies.Domain.Entities
{
    using TravelBuddies.Domain.EntityModels;

    public class City : BaseSoftDeleteEntity<int>
	{
		public required string Name { get; set; }

		public int CountryId { get; set; }
		public required Country Country { get; set; }
	}
}
