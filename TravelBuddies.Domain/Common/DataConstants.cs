namespace TravelBuddies.Domain.Common
{
    public static class DataConstants
    {
        public static class MessageConstants
        {
            public const int MinLengthText = 1;
            public const int MaxLengthText = 5000;
        }

        public static class PostConstants
        {
            public const int MaxLengthDestination = 100;
            public const int MinLengthDestination = 1;

            public const int MaxLengthDescription = 5000;
            public const int MinLengthDescription = 50;

            public const int MaxLengthCurrency = 3;
        }

        public static class ReviewConstants
        {
            public const int MinLengthText = 3;
            public const int MaxLengthText = 2000;
        }

        public static class RoleConstants
        {
            public const int MaxLengthName = 50;
            public const int MinLengthName = 3;
        }

        public static class UserConstants
        {
            public const int MinLengthEmail = 3;
            public const int MaxLengthEmail = 345;

            public const int MinLengthPassword = 8;
            public const int MaxLengthPassword = 16;

            public const int MaxLengthFirstName = 50;
            public const int MinLengthFirstName = 3;

            public const int MaxLengthLastName = 50;
            public const int MinLengthLastName = 3;
        }

        public static class VehicleConstants
        {
            public const int MaxLengthBrandName = 50;
            public const int MinLengthBrandName = 1;

            public const int MaxLengthModelName = 70;
            public const int MinLengthModelName = 1;

            public const int MaxRangeFuel = 2;
            public const int MinRangeFule = 0;

            public const int MaxLengthColor = 30;
            public const int MinLengthColor = 1;
        }

        public static class CountryConstants
        {
            public const int MaxLengthCountryName = 60;
            public const int MinLengthCountryName = 1;
        }

        public static class CityConstants
        {
            public const int MaxLengthCityName = 100;
            public const int MinLengthCityName = 1;
        }

        public static class LogConstants
        {
            public const int MaxLengthMessage = 500;
        }
    }
}
