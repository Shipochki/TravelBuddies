namespace TravelBuddies.Application.Common.Extensions
{
	public static class DateTimeExtensions
	{
		public static string TimeAgo(this DateTime pastDate)
		{
			DateTime currentDate = DateTime.Now;
			TimeSpan timeSpan = currentDate - pastDate;

			if (timeSpan.TotalSeconds < 60)
			{
				return $"{(int)timeSpan.TotalSeconds} second{(timeSpan.TotalSeconds != 1 ? "s" : "")} ago";
			}
			else if (timeSpan.TotalMinutes < 60)
			{
				return $"{(int)timeSpan.TotalMinutes} minute{(timeSpan.TotalMinutes != 1 ? "s" : "")} ago";
			}
			else if (timeSpan.TotalHours < 24)
			{
				return $"{(int)timeSpan.TotalHours} hour{(timeSpan.TotalHours != 1 ? "s" : "")} ago";
			}
			else if (timeSpan.TotalDays < 30)
			{
				return $"{(int)timeSpan.TotalDays} day{(timeSpan.TotalDays != 1 ? "s" : "")} ago";
			}
			else
			{
				int monthsDifference = (currentDate.Year - pastDate.Year) * 12 + currentDate.Month - pastDate.Month;

				if (currentDate.Day < pastDate.Day)
				{
					monthsDifference--;
				}

				int yearsDifference = monthsDifference / 12;
				monthsDifference %= 12;

				if (yearsDifference > 0)
				{
					return $"{yearsDifference} year{(yearsDifference != 1 ? "s" : "")} ago";
				}
				else
				{
					return $"{monthsDifference} month{(monthsDifference != 1 ? "s" : "")} ago";
				}
			}
		}
	}
}
