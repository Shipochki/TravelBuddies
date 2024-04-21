﻿namespace TravelBuddies.Application.Exceptions
{
	public class ApplicationUserNotFoundException : BaseException
	{
        public ApplicationUserNotFoundException(string message)
            : base(message)
        {
            
        }

        public ApplicationUserNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
