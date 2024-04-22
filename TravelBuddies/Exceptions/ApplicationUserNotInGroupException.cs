﻿namespace TravelBuddies.Application.Exceptions
{
	public class ApplicationUserNotInGroupException : BaseException
	{
		public ApplicationUserNotInGroupException(string message)
			: base(message)
		{
		}

		public ApplicationUserNotInGroupException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}