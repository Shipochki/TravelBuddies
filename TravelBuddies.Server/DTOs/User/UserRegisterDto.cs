﻿namespace TravelBuddies.Presentation.DTOs.User
{
    using System.ComponentModel.DataAnnotations;
    using static TravelBuddies.Domain.Common.DataConstants.UserConstants;
    using static TravelBuddies.Domain.Common.DataConstants.CityConstants;
    using static TravelBuddies.Domain.Common.DataConstants.CountryConstants;

    public class UserRegisterDto
    {
        [Required]
        [MinLength(MinLengthEmail)]
        [MaxLength(MaxLengthEmail)]
        public required string Email { get; set; }

        [Required]
        [MinLength(MinLengthPassword)]
        [MaxLength(MaxLengthPassword)]
        public required string Password { get; set; }

        [Required]
        [MinLength(MinLengthFirstName)]
        [MaxLength(MaxLengthFirstName)]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(MinLengthLastName)]
        [MaxLength(MaxLengthLastName)]
        public required string LastName { get; set; }

        [MinLength(MinLengthCountryName)]
        [MaxLength(MaxLengthCountryName)]
        public string? Country { get; set; }

        [MinLength(MinLengthCityName)]
        [MaxLength(MaxLengthCityName)]
        public string? City { get; set; }

        public IFormFile? ProfilePicture { get; set; }
    }
}
