namespace TravelBuddies.Domain.Entities
{
	using Microsoft.AspNetCore.Identity;
    using TravelBuddies.Domain.EntityModels;

    public class ApplicationUser : IdentityUser, ISoftDeleteEntity, IActionInfo
	{
        public ApplicationUser()
        {
			this.Id = Guid.NewGuid().ToString();	
        }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? ProfilePictureLink { get; set; }

		public string? DriverLicenseFrontPictureLink { get; set; }

		public string? DriverLicenseBackPictureLink { get; set; }

		public string? Country { get; set; }

		public string? City { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public DateTime CreatedOn { get ; set ; }

		public DateTime? UpdatedOn { get ; set ; }
	}
}
