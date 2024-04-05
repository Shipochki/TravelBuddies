namespace TravelBuddies.Domain
{
	public static class DataConstants
	{
		public static class MessageConstants
		{
			public const int MaxLengthText = 5000;
		}

		public static class PostConstants
		{
			public const int MaxLengthDestination = 100;
			public const int MinLengthDestination = 1;

			public const int MaxLengthDescription = 5000;
			public const int MinLengthDescription = 200;
		}

		public static class ReviewConstants
		{
			public const int MaxLengthText = 2000;
		}

		public static class RoleConstants
		{
			public const int MaxLengthName = 50;
			public const int MinLengthName = 3;
		}

		public static class UserConstants
		{
			public const int MaxLengthFirstName = 50;
			public const int MinLengthFirstName = 3;

			public const int MaxLengthLastName = 50;
			public const int MinLengthLastName = 3;

			public const int MaxLengthPhoneNumber = 15;
			public const int MinLengthPhoneNumber = 7;

			public const int MaxLengthEmail = 100;
			public const int MinLengthEmail = 3;

			public const int MaxLengthCountryName = 60;
			public const int MinLengthCountryName = 1;

			public const int MaxLengthCityName = 100;
			public const int MinLengthCityName = 1;
		}
	}
}
