namespace TravelBuddies.Application.Common.Exceptions.Messages
{
    public static class ExceptionMessages
    {
        public const string ApplicationUserNotFoundMessage = "Non-extitent User with Id {0}";
        public const string ApplicationUserNotCreatorMessage = "User with Id {0} is not creator";
        public const string ApplicationUserNotInGroupMessage = "User with Id {0} is not in group with Id {1}";
        public const string ApplicationUserAllreadyInGroupMessage = "User with Id {0} is allready in Group with Id {1}";
        public const string ApplicationUserAllreadyIsBannedFromGroup = "User with Id {0} is allready banned from group with id {1}";
        public const string UnableToCreateApplicationUserMessage = "Unable to create user";
        public const string UnableToAddRoleToUserMessage = "Unable to add role to user with Id {0}";
        public const string InvalidLoginMessage = "Unable to login";

        public const string IdentityRoleNotFoundMessage = "Role with name: {0} not found";

        public const string PostNotFoundMessage = "Non-extitent Post with Id {0}";
        public const string NotAvailableSeatsInPostMessage = "No available seats in group with id {0}";

        public const string GroupNotFoundMessage = "Non-extitent Group with Id {0}";
        public const string GroupNotMatchMessage = "Message group is not matching";

        public const string MessageNotFoundMessage = "Non-extitent Message with Id {0}";

        public const string CityNotFoundMessage = "Non-extitent City with Id {0}";

        public const string ReviewNotFoundMessage = "Non-extitent Review with Id {0}";

        public const string VehicleNotFoundMessage = "Vehicle with id {0} not found";
    }
}
