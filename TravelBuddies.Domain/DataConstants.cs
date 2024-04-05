namespace TravelBuddies.Domain
{
	public static class DataConstants
	{
		public static class Message
		{
			public const int MaxLengthText = 5000;
		}

		public static class Post
		{
			public const int MaxLengthDestination = 100;
			public const int MinLengthDestination = 1;

			public const int MaxLengthDescription = 5000;
			public const int MinLengthDescription = 200;
		}
	}
}
