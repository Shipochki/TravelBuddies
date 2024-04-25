using TravelBuddies.Domain.Common;

namespace TravelBuddies.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.UserConstants;
    using static DataConstants.Country;
    using static DataConstants.City;
    using Microsoft.AspNetCore.Identity;
    using TravelBuddies.Domain.EntityModels;

    public class ApplicationUser : IdentityUser, ISoftDeleteEntity, IActionInfo
	{
        public ApplicationUser()
        {
			this.Id = Guid.NewGuid().ToString();	
        }

		[MaxLength(MaxLengthFirstName)]
		public string? FirstName { get; set; }

		[MaxLength(MaxLengthLastName)]
		public string? LastName { get; set; }

		public string? ProfilePictureLink { get; set; }

		public string? DriverLicenseFrontPictureLink { get; set; }

		public string? DriverLicenseBackPictureLink { get; set; }

		[MaxLength(MaxLengthCountryName)]
		public string? Country { get; set; }

		[MaxLength(MaxLengthCityName)]
		public string? City { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime DeletedOn { get; set; }

		public DateTime CreatedOn { get ; set ; }

		public DateTime? UpdatedOn { get ; set ; }
	}
}
