namespace TravelBuddies.Domain.Entities
{
    using TravelBuddies.Domain.EntityModels;

    public class Country : BaseSoftDeleteEntity<int>
	{
		public required string Name { get; set; }
	}
}
