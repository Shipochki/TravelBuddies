namespace TravelBuddies.Domain.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using static DataConstants.UserConstants;
	using static DataConstants.Country;
	using static DataConstants.City;
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Domain.Models;

	public class ApplicationUser : IdentityUser, ISoftDeleteEntity
	{
        public ApplicationUser()
        {
			this.Id = new Guid().ToString();
        }

        [Required]
		[MaxLength(MaxLengthFirstName)]
		public required string FirstName { get; set; }

		[Required]
		[MaxLength(MaxLengthLastName)]
		public required string LastName { get; set; }

		[Required]
		public required string ProfilePictureLink { get; set; }

		public string? DriverLicenseFrontPictureLink { get; set; }

		public string? DriverLicenseBackPictureLink { get; set; }

		[MaxLength(MaxLengthCountryName)]
		public string? Country { get; set; }

		[MaxLength(MaxLengthCityName)]
		public string? City { get; set; }

		[ForeignKey(nameof(Vehicle))]
		public int? VehicleId { get; set; }
		public Vehicle? Vehicle { get; set; }

		public List<Review> RecivedReviews { get; set; } = new List<Review>();

		public List<Review> CreatedReviews { get; set; } = new List<Review>();

		public List<Message> Messages { get; set; } = new List<Message>();

		public List<UserGroup> UsersGroups { get; set; } = new List<UserGroup>();

		public List<Post> Posts { get; set; } = new List<Post>();

		public List<Group> Groups { get; set; } = new List<Group>();

		public bool IsDeleted { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
