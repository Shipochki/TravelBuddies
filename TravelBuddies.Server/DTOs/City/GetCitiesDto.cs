namespace TravelBuddies.Presentation.DTOs.City
{
	using TravelBuddies.Domain.Entities;

	public class GetCitiesDto
	{
		public int Id { get; set; }

		public required string Name { get; set; }

		public static GetCitiesDto FromCity(City city) 
		{
			return new GetCitiesDto()
			{
				Id = city.Id,
				Name = city.Name,
			};
		}
	}
}
